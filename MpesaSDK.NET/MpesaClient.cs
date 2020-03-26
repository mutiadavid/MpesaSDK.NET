using MpesaSDK.NET.Dtos;
using MpesaSDK.NET.Dtos.Requests;
using MpesaSDK.NET.Dtos.Responses;
using MpesaSDK.NET.Enums;
using MpesaSDK.NET.Validators;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Text;
using System.Threading.Tasks;

namespace MpesaSDK.NET
{
    public class MpesaClient
    {
        private string _accessToken;
        private DateTime _tokenExpiresIn;
        private readonly string _consumerKey, _consumerSecret;
        private readonly bool _isSandBox;
        public MpesaClient(string consumerKey, string consumerSecret, bool sandbox = true)
        {
            _accessToken = "";
            _tokenExpiresIn = DateTime.Now.AddMinutes(-1);
            _consumerKey = consumerKey;
            _consumerSecret = consumerSecret;
            _isSandBox = sandbox;
        }

        #region constant URLs
        private string baseUrl => _isSandBox ? "https://sandbox.safaricom.co.ke" : "https://api.safaricom.co.ke";
        private string oauthUrl => "oauth/v1/generate?grant_type=client_credentials";

        #endregion

        #region private methods
        private async Task<string> GetAccessToken()
        {
            var now = DateTime.Now;
            if (_tokenExpiresIn > now)
            {
                return _accessToken;
            }

            try
            {
                RestClient restClient = new RestClient(baseUrl)
                {
                    Authenticator = new HttpBasicAuthenticator(_consumerKey, _consumerSecret)
                };
                RestRequest restRequest = new RestRequest(oauthUrl, Method.GET);
                restRequest.AddHeader("Content-Type", "application/json");
                var response = await restClient.ExecuteTaskAsync<AccessTokenDto>(restRequest);
                if (!response.IsSuccessful)
                {
                    HandleRestExceptions(response, "Error getting access token");
                }

                _tokenExpiresIn = now.AddSeconds(response.Data.ExpiresIn).AddMinutes(-1);
                _accessToken = response.Data.AccessToken;
                return _accessToken;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }

        private async Task<(S SuccessResponse, ErrorResponse ErrorResponse, bool IsSuccessful)> PostRequestAsync<S>(string resourceUrl, string data) where S : new()
        {
            try
            {
                var token = await GetAccessToken();
                RestClient restClient = new RestClient(baseUrl);
                RestRequest restRequest = new RestRequest(resourceUrl, Method.POST);
                restRequest.AddHeader("Content-Type", "application/json");
                restRequest.AddHeader("Authorization", $"Bearer {token}");
                restRequest.AddParameter("application/json;charset=utf-8", data, ParameterType.RequestBody);
                var response = await restClient.ExecuteTaskAsync(restRequest);

                //var result = new MpesaResponse();
                (S SuccessResponse, ErrorResponse ErrorResponse, bool IsSuccessful) result = (new S(), null, false);
                if (!response.IsSuccessful)
                {
                    result.ErrorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
                    result.IsSuccessful = false;
                    //try
                    //{
                    //    result.ErrorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
                    //    result.IsSuccess = false;
                    //}
                    //catch (Exception)
                    //{
                    //    HandleRestExceptions(response, "Error sending post request");
                    //}
                }
                else
                {

                    result.SuccessResponse = JsonConvert.DeserializeObject<S>(response.Content);
                    result.IsSuccessful = true;
                    //try
                    //{
                    //    //try parsing success response
                    //    result.SuccessResponse = JsonConvert.DeserializeObject<SuccessResponse>(response.Content);
                    //    result.IsSuccess = true;
                    //}
                    //catch
                    //{
                    //    result.ErrorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
                    //    result.IsSuccess = false;
                    //}
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void HandleRestExceptions(IRestResponse response, string message)
        {
            message += ". " +
                (string.IsNullOrWhiteSpace(response.ErrorMessage) ? "" : response.ErrorMessage + ". ") +
                (response.ErrorException != null ? response.ErrorException.Message : "");
            throw new Exception(message);
        }

        private async Task<MpesaResponse> CommonMpesaPostAsync(string url, string payload)
        {
            var res = await PostRequestAsync<CommonSuccessResponse>(url, payload);
            return new MpesaResponse()
            {
                ErrorResponse = res.ErrorResponse,
                SuccessResponse = res.SuccessResponse,
                IsSuccess = res.IsSuccessful
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<StkPushResponse> STKPushAsync(STKPushRequest request)
        {
            this.ValidatePhoneNumber(request.PhoneNumber);
            SameValueValidator.ValidateSameValue(request.PhoneNumber, request.PartyA, "PatyA", "PhoneNumber");

            this.ValidateTimestamp(request.Timestamp);

            this.ValidateBusinessShortCode(request.BusinessShortCode);
            SameValueValidator.ValidateSameValue(request.PartyB, request.BusinessShortCode, "PartyB", "BusinessShortCode");

            LengthValidator.ValidateLength(request.AccountReference, "AccountReference", 12);

            LengthValidator.ValidateLength(request.TransactionDesc, "TransactionDesc", 13, 1);

            URLValidator.ValidateURL(request.CallBackURL, "CallBackURL");

            var response = await PostRequestAsync<StkPushSuccessResponse>("mpesa/stkpush/v1/processrequest", request.ToString());

            return new StkPushResponse()
            {
                ErrorResponse = response.ErrorResponse,
                SuccessResponse = response.SuccessResponse,
                IsSuccess = response.IsSuccessful
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<StkPushQueryResponse> StkPushQueryAsync(StkPushQueryRequest request)
        {
            var response = await PostRequestAsync<StkPushQuerySuccessResponse>("mpesa/stkpushquery/v1/query", request.ToString());

            return new StkPushQueryResponse()
            {
                ErrorResponse = response.ErrorResponse,
                SuccessResponse = response.SuccessResponse,
                IsSuccess = response.IsSuccessful
            };

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private Task<MpesaResponse> B2CAsync(B2CRequest request)
        {
            //TODO: Validations
            return CommonMpesaPostAsync("mpesa/b2c/v1/paymentrequest", request.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private Task<MpesaResponse> B2BAsync(B2BRequest request)
        {
            return CommonMpesaPostAsync("mpesa/b2b/v1/paymentrequest", request.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private Task<MpesaResponse> C2BRegisterUrlAsync(C2BRegisterURLRequest request)
        {
            return CommonMpesaPostAsync("mpesa/c2b/v1/registerurl", request.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private Task<MpesaResponse> C2BSimulateTransactionAsync(C2BSimulateTransactionRequest request)
        {
            return CommonMpesaPostAsync("mpesa/c2b/v1/simulate", request.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private Task<MpesaResponse> AccountBalanceAsync(AccountBalanceRequest request)
        {
            return CommonMpesaPostAsync("mpesa/accountbalance/v1/query", request.ToString());
        }

        private Task<MpesaResponse> TransactionStatusAsync(TransactionStatusRequest request)
        {
            return CommonMpesaPostAsync("mpesa/transactionstatus/v1/query", request.ToString());
        }

        private Task<MpesaResponse> ReversalAsync(ReversalRequest request)
        {
            return CommonMpesaPostAsync("mpesa/reversal/v1/request", request.ToString());
        }
        #endregion

        #region Public methods

        /// <summary>
        /// Use this API to initiate online payment on behalf of a customer.
        /// </summary>
        /// <param name="businessCode">The organization receiving the funds. The parameter expected is a 5 to 6 digit as defined on the Shortcode description above. This can be the same as BusinessShortCode value above</param>
        /// <param name="phoneNumber">The phone number sending money. The parameter expected is a Valid Safaricom Mobile Number that is M-Pesa registered in the format 2547XXXXXXXX</param>
        /// <param name="amount">The amount to be transacted.</param>
        /// <param name="passKey">PassKey from safaricom portal</param>
        /// <param name="callbackUrl">A CallBack URL is a valid secure URL that is used to receive notifications from M-Pesa API. It is the endpoint to which the results will be sent by M-Pesa API.</param>
        /// <param name="accountReference">Account Reference: This is an Alpha-Numeric parameter that is defined by your system as an Identifier of the transaction for CustomerPayBillOnline transaction type. Along with the business name, this value is also displayed to the customer in the STK Pin Prompt message. Maximum of 12 characters.</param>
        /// <param name="transactionDesc">This is any additional information/comment that can be sent along with the request from your system. Maximum of 13 Characters.</param>
        /// <param name="command">This is the transaction type that is used to identify the transaction when sending the request to M-Pesa. The transaction type for M-Pesa Express is "CustomerPayBillOnline" </param>
        /// <returns></returns>
        public Task<StkPushResponse> STKPushAsync(string businessCode, string phoneNumber, long amount, string passKey, string callbackUrl, string accountReference = "12345", string transactionDesc = "Payment", Command command = Command.CustomerPayBillOnline)
        {
            var (password, timestamp) = HelperFunctions.MpesaPassword(passKey, businessCode);

            return STKPushAsync(new STKPushRequest()
            {
                BusinessShortCode = businessCode,
                PartyA = phoneNumber,
                PartyB = businessCode,
                PhoneNumber = phoneNumber,
                TransactionType = command.ToString(),
                TransactionDesc = transactionDesc,
                Amount = amount,
                CallBackURL = callbackUrl,
                AccountReference = accountReference,
                Password = password,
                Timestamp = timestamp
            });
        }

        /// <summary>
        /// Use this API to check the status of a Lipa Na M-Pesa Online Payment
        /// </summary>
        /// <param name="businessShortCode">This is organizations shortcode (Paybill or Buygoods - A 5 to 6 digit account number) used to identify an organization and receive the transaction.</param>
        /// <param name="checkoutRequestID">This is a global unique identifier of the processed checkout transaction request.; ws_CO_DMZ_123212312_2342347678234.</param>
        /// <param name="passkey">passkey: from safaricom</param>
        /// <returns></returns>
        public Task<StkPushQueryResponse> StkPushQueryAsync(string businessShortCode, string checkoutRequestID, string passkey)
        {
            var (password, timestamp) = HelperFunctions.MpesaPassword(passkey, businessShortCode);

            return StkPushQueryAsync(new StkPushQueryRequest()
            {
                BusinessShortCode = businessShortCode,
                CheckoutRequestID = checkoutRequestID,
                Password = password,
                Timestamp = timestamp
            });
        }

        /// <summary>
        /// Use this API to transact between an M-Pesa short code to a phone number registered on M-Pesa.
        /// </summary>
        /// <param name="initiatorName">The username of the M-Pesa B2C account API operator. 
        /// NOTE: the access channel for this operator myst be API and the account must be in active status
        ///</param>
        /// <param name="initiatorPassword">Initiator Password</param>
        /// <param name="command">This is a unique command that specifies B2C transaction type.
        /// SalaryPayment: This supports sending money to both registere and unregistered M-Pesa customers.
        /// BusinessPayment: This is a normal business to customer payment, supports only M-Pesa registered customers.
        /// PromotionPayment: This is a promotional payment to customers.The M-Pesa notification message is a congratulatory message.Supports only M-Pesa registered customers.
        ///</param>
        /// <param name="amount">The amount of money being sent to the customer.</param>
        /// <param name="partyA">This is the B2C organization shortcode from which the money is to be sent.</param>
        /// <param name="partyB">This is the customer mobile number  to receive the amount. - The number should have the country code (254) without the plus sign.</param>
        /// <param name="remarks">Any additional information to be associated with the transaction.</param>
        /// <param name="queueTimeOutURL">This is the URL to be specified in your request that will be used by API Proxy to send notification incase the payment request is timed out while awaiting processing in the queue. </param>
        /// <param name="resultURL">This is the URL to be specified in your request that will be used by M-Pesa to send notification upon processing of the payment request.</param>
        /// <param name="occassion">Any additional information to be associated with the transaction.</param>
        /// <returns></returns>
        public Task<MpesaResponse> B2CAsync(string initiatorName, string initiatorPassword, Command command, long amount, string partyA, string partyB, string remarks, string queueTimeOutURL, string resultURL, string occassion)
        {
            return B2CAsync(new B2CRequest()
            {
                InitiatorName = initiatorName,
                SecurityCredential = initiatorPassword.ToMpesaSecurityCredential(),
                CommandID = command.ToString(),
                Amount = amount,
                Occasion = occassion,
                PartyA = partyA,
                PartyB = partyB,
                QueueTimeOutURL = queueTimeOutURL,
                Remarks = remarks,
                ResultURL = resultURL
            });
        }

        /// <summary>
        /// This API enables Business to Business (B2B) transactions between a business and another business. Use of this API requires a valid and verified B2B M-Pesa short code for the business initiating the transaction and the both businesses involved in the transaction.
        /// </summary>
        /// <param name="initiator">This is the credential/username used to authenticate the transaction request.</param>
        /// <param name="initiatorPassword">Initiator Password</param>
        /// <param name="command">Unique command for each transaction type, possible values are: BusinessPayBill, MerchantToMerchantTransfer, MerchantTransferFromMerchantToWorking, MerchantServicesMMFAccountTransfer, AgencyFloatAdvance</param>
        /// <param name="amount">The amount being transacted.</param>
        /// <param name="partyA">Organization’s short code initiating the transaction.</param>
        /// <param name="partyB">Organization’s short code receiving the funds being transacted.</param>
        /// <param name="senderIdentifier">Type of organization sending the transaction.</param>
        /// <param name="recieverIdentifierType">Type of organization receiving the funds being transacted.</param>
        /// <param name="remarks">Comments that are sent along with the transaction.</param>
        /// <param name="queueTimeOutURL">The path that stores information of time out transactions.it should be properly validated to make sure that it contains the port, URI and domain name or publicly available IP.</param>
        /// <param name="resultURL">The path that receives results from M-Pesa it should be properly validated to make sure that it contains the port, URI and domain name or publicly available IP.</param>
        /// <param name="accountReference">Account Reference mandatory for “BusinessPaybill” CommandID.</param>
        /// <returns></returns>
        public Task<MpesaResponse> B2BAsync(string initiator, string initiatorPassword, Command command, long amount, string partyA, string partyB, IdentifierType senderIdentifier, IdentifierType recieverIdentifierType, string remarks, string queueTimeOutURL, string resultURL, string accountReference)
        {
            return B2BAsync(new B2BRequest
            {
                Initiator = initiator,
                PartyA = partyA,
                PartyB = partyB,
                CommandID = command.ToString(),
                Amount = amount,
                AccountReference = accountReference,
                SecurityCredential = initiatorPassword.ToMpesaSecurityCredential(),
                Remarks = remarks,
                QueueTimeOutURL = queueTimeOutURL,
                ResultURL = resultURL,
                RecieverIdentifierType = recieverIdentifierType.ToString("d"),
                SenderIdentifier = senderIdentifier.ToString("d")
            });
        }

        /// <summary>
        /// Use this API to register validation and confirmation URLs on M-Pesa 
        /// </summary>
        /// <param name="validationURL">This is the URL that receives the validation request from API upon payment submission. 
        /// The validation URL is only called if the external validation on the registered shortcode is enabled.
        /// (By default External Validation is dissabled)
        ///</param>
        /// <param name="confirmationURL">This is the URL that receives the confirmation request from API upon payment completion.  </param>
        /// <param name="responseType">This parameter specifies what is to happen if for any reason the validation URL is nor reachable. 
        /// Note that, This is the default action value that determines what MPesa will do in the scenario that your endpoint is unreachable or is unable to respond on time. 
        /// Only two values are allowed: Completed or Cancelled. 
        /// Completed means MPesa will automatically complete your transaction, 
        /// whereas Cancelled means MPesa will automatically cancel the transaction, in the event MPesa is unable to reach your Validation URL.
        ///</param>
        /// <param name="shortCode">The short code of the organization.</param>
        /// <returns></returns>
        public Task<MpesaResponse> C2BRegisterUrlAsync(string validationURL, string confirmationURL, ResponseType responseType, string shortCode)
        {
            return C2BRegisterUrlAsync(new C2BRegisterURLRequest()
            {
                ValidationURL = validationURL,
                ConfirmationURL = confirmationURL,
                ShortCode = shortCode,
                ResponseType = responseType.ToString()
            });
        }

        /// <summary>
        /// This API is used to make payment requests from Client to Business (C2B). You can use the sandbox provided test credentials down below to simulates a payment made from the client phone's STK/SIM Toolkit menu, and enables you to receive the payment requests in real time. 
        /// </summary>
        /// <param name="command">This is a unique identifier of the transaction type: There are two types of these Identifiers:
        ///CustomerPayBillOnline: This is used for Pay Bills shortcodes.
        ///CustomerBuyGoodsOnline: This is used for Buy Goods shortcodes.
        ///</param>
        /// <param name="amount">This is the amount being transacted. The parameter expected is a numeric value.</param>
        /// <param name="msisdn">This is the phone number initiating the C2B transaction.</param>
        /// <param name="billRefNumber">This is used on CustomerPayBillOnline option only. This is where a customer is expected to enter a unique bill identifier, e.g an Account Number. </param>
        /// <param name="shortCode">This is the Short Code receiving the amount being transacted.</param>
        /// <returns></returns>
        public Task<MpesaResponse> C2BSimulateTransactionAsync(Command command, long amount, string msisdn, string billRefNumber, string shortCode)
        {
            return C2BSimulateTransactionAsync(new C2BSimulateTransactionRequest()
            {
                CommandID = command.ToString(),
                MSISDN = msisdn,
                Amount = amount,
                BillRefNumber = billRefNumber,
                ShortCode = shortCode
            });
        }

        /// <summary>
        /// Use this API to enquire the balance on an M-Pesa BuyGoods (Till Number).
        /// </summary>
        /// <param name="partyA">Organization receiving the transaction</param>
        /// <param name="identifierType">Type of organization receiving the transaction</param>
        /// <param name="initiator">The name of Initiator to initiating  the request</param>
        /// <param name="initiatorPassword">Initiator Password</param>
        /// <param name="queueTimeOutURL">The path that stores information of time out transaction</param>
        /// <param name="resultURL">The path that stores information of transaction </param>
        /// <param name="remarks">Comments that are sent along with the transaction.</param>
        /// <param name="command">Takes only 'AccountBalance' CommandID</param>
        /// <returns></returns>
        public Task<MpesaResponse> AccountBalanceAsync(string partyA, IdentifierType identifierType, string initiator, string initiatorPassword, string queueTimeOutURL, string resultURL, string remarks = "Account Balance Check", Command command = Command.AccountBalance)
        {
            return AccountBalanceAsync(new AccountBalanceRequest()
            {
                //AccountType = identifierType.ToString("d"),
                IdentifierType = identifierType.ToString("d"),
                PartyA = partyA,
                CommandID = command.ToString(),
                Initiator = initiator,
                SecurityCredential = initiatorPassword.ToMpesaSecurityCredential(),
                Remarks = remarks,
                QueueTimeOutURL = queueTimeOutURL,
                ResultURL = resultURL
            });
        }

        /// <summary>
        /// Takes only 'TransactionStatusQuery' command id
        /// </summary>
        /// <param name="partyA">Organization/MSISDN receiving the transaction</param>
        /// <param name="identifierType">Type of organization receiving the transaction</param>
        /// <param name="remarks"></param>
        /// <param name="initiator"></param>
        /// <param name="initiatorPassword"></param>
        /// <param name="queueTimeOutURL"></param>
        /// <param name="resultURL"></param>
        /// <param name="transactionID"></param>
        /// <param name="occasion"></param>
        /// <param name="command">Takes only 'TransactionStatusQuery' command id</param>
        /// <returns></returns>
        public Task<MpesaResponse> TransactionStatusAsync(string partyA, IdentifierType identifierType, string remarks, string initiator, string initiatorPassword, string queueTimeOutURL, string resultURL, string transactionID, string occasion = "", Command command = Command.TransactionStatusQuery)
        {
            return TransactionStatusAsync(new TransactionStatusRequest()
            {
                PartyA = partyA,
                ShortCode = partyA,
                CommandID = command.ToString(),
                IdentifierType = identifierType.ToString("d"),
                Initiator = initiator,
                SecurityCredential = initiatorPassword.ToMpesaSecurityCredential(),
                TransactionID = transactionID,
                Occasion = occasion,
                Remarks = remarks,
                QueueTimeOutURL = queueTimeOutURL,
                ResultURL = resultURL
            });
        }

        /// <summary>
        /// Transaction Reversal API reverses a M-Pesa transaction.
        /// </summary>
        /// <param name="initiator">The name of Initiator to initiating  the request; Organization/MSISDN sending the transaction.</param>
        /// <param name="initiatorPassword">password for the initiator to authenticate the transaction request</param>
        /// <param name="receiverParty">Organization receiving the transaction; -Shortcode (6 digits)</param>
        /// <param name="receiverIdentifierType">Type of organization receiving the transaction</param>
        /// <param name="transactionID">Transaction to reverse; E.g. LKXXXX1234</param>
        /// <param name="queueTimeOutURL">The path/url endpoint that stores information of time out transaction</param>
        /// <param name="resultURL">The path/url endpoint that stores information of transaction </param>
        /// <param name="remarks">Comments that are sent along with the transaction.; sequence of characters up to 100</param>
        /// <param name="command">Takes only 'TransactionReversal' Command id; TransactionReversal</param>
        /// <param name="occasion">Optional Parameter; sequence of characters up to 100.</param>
        /// <returns></returns>
        public Task<MpesaResponse> ReversalAsync(string initiator, string initiatorPassword, string receiverParty, IdentifierType receiverIdentifierType, string transactionID, long amount, string queueTimeOutURL, string resultURL, string remarks = "Reversal", Command command = Command.TransactionReversal, string occasion = "")
        {
            return ReversalAsync(new ReversalRequest()
            {
                CommandID = command.ToString(),
                SecurityCredential = initiatorPassword.ToMpesaSecurityCredential(),
                Initiator = initiator,
                ReceiverParty = receiverParty,
                RecieverIdentifierType = receiverIdentifierType.ToString(),
                TransactionID = transactionID,
                Amount = amount,
                ResultURL = resultURL,
                QueueTimeOutURL = queueTimeOutURL,
                Occasion = occasion,
                Remarks = remarks
            });
        }

        #endregion
    }
}