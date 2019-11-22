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

        #endregion

        #region Public methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<(StkPushSuccessResponse StkPushSuccessResponse, ErrorResponse ErrorResponse, bool IsSuccessful)> STKPushAsync(STKPushRequest request)
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

            //return new StkPushResponse()
            //{
            //    ErrorResponse = response.ErrorResponse,
            //    SuccessResponse = response.SuccessResponse,
            //    IsSuccess = response.IsSuccessful
            //};

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessCode"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="amount"></param>
        /// <param name="passKey"></param>
        /// <param name="callbackUrl"></param>
        /// <param name="accountReference"></param>
        /// <param name="transactionDesc"></param>
        /// <param name="transactionType"></param>
        /// <returns></returns>
        public Task<(StkPushSuccessResponse StkPushSuccessResponse, ErrorResponse ErrorResponse, bool IsSuccessful)> STKPushAsync(string businessCode, string phoneNumber, long amount, string passKey, string callbackUrl, string accountReference = "12345", string transactionDesc = "Payment", Command command = Command.CustomerPayBillOnline)
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
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<(StkPushQuerySuccessResponse StkPushQueryRequest, ErrorResponse ErrorResponse, bool IsSuccessful)> StkPushQueryAsync(StkPushQueryRequest request)
        {
            var response = await PostRequestAsync<StkPushQuerySuccessResponse>("mpesa/stkpushquery/v1/query", request.ToString());

            //return new StkPushQueryResponse()
            //{
            //    ErrorResponse = response.ErrorResponse,
            //    SuccessResponse = response.SuccessResponse,
            //    IsSuccess = response.IsSuccessful
            //};

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessShortCode"></param>
        /// <param name="checkoutRequestID"></param>
        /// <param name="passkey"></param>
        /// <returns></returns>
        public Task<(StkPushQuerySuccessResponse StkPushQueryRequest, ErrorResponse ErrorResponse, bool IsSuccessful)> StkPushQueryAsync(string businessShortCode, string checkoutRequestID, string passkey)
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
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<MpesaResponse> B2CAsync(B2CRequest request)
        {
            //TODO: Validations
            return CommonMpesaPostAsync("mpesa/b2c/v1/paymentrequest", request.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initiatorName"></param>
        /// <param name="initiatorPassword"></param>
        /// <param name="command"></param>
        /// <param name="amount"></param>
        /// <param name="partyA"></param>
        /// <param name="partyB"></param>
        /// <param name="remarks"></param>
        /// <param name="queueTimeOutURL"></param>
        /// <param name="resultURL"></param>
        /// <param name="occassion"></param>
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
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<MpesaResponse> B2BAsync(B2BRequest request)
        {
            return CommonMpesaPostAsync("mpesa/b2b/v1/paymentrequest", request.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initiator"></param>
        /// <param name="initiatorPassword"></param>
        /// <param name="command"></param>
        /// <param name="amount"></param>
        /// <param name="partyA"></param>
        /// <param name="partyB"></param>
        /// <param name="senderIdentifier"></param>
        /// <param name="recieverIdentifierType"></param>
        /// <param name="remarks"></param>
        /// <param name="queueTimeOutURL"></param>
        /// <param name="resultURL"></param>
        /// <param name="accountReference"></param>
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


        public Task<MpesaResponse> C2BRegisterUrlAsync(C2BRegisterURLRequest request)
        {
            return CommonMpesaPostAsync("mpesa/c2b/v1/registerurl", request.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="validationURL"></param>
        /// <param name="confirmationURL"></param>
        /// <param name="responseType"></param>
        /// <param name="shortCode"></param>
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
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<MpesaResponse> C2BSimulateTransactionAsync(C2BSimulateTransactionRequest request)
        {
            return CommonMpesaPostAsync("mpesa/c2b/v1/simulate", request.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="amount"></param>
        /// <param name="msisdn"></param>
        /// <param name="billRefNumber"></param>
        /// <param name="shortCode"></param>
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
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<MpesaResponse> AccountBalanceAsync(AccountBalanceRequest request)
        {
            return CommonMpesaPostAsync("mpesa/accountbalance/v1/query", request.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="partyA"></param>
        /// <param name="identifierType"></param>
        /// <param name="initiator"></param>
        /// <param name="initiatorPassword"></param>
        /// <param name="queueTimeOutURL"></param>
        /// <param name="resultURL"></param>
        /// <param name="remarks"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public Task<MpesaResponse> AccountBalanceAsync(string partyA, IdentifierType identifierType, string initiator, string initiatorPassword, string queueTimeOutURL, string resultURL, string remarks = "Account Balance Check", Command command = Command.AccountBalance)
        {
            return AccountBalanceAsync(new AccountBalanceRequest()
            {
                AccountType = identifierType.ToString("d"),
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

        public Task<MpesaResponse> TransactionStatusAsync(TransactionStatusRequest request)
        {
            return CommonMpesaPostAsync("mpesa/transactionstatus/v1/query", request.ToString());
        }

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

        public Task<MpesaResponse> ReversalAsync(ReversalRequest request)
        {
            return CommonMpesaPostAsync("mpesa/reversal/v1/request", request.ToString());
        }

        #endregion
    }
}