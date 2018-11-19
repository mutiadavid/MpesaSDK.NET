using MpesaSDK.NET.Dtos;
using MpesaSDK.NET.Dtos.Requests;
using MpesaSDK.NET.Dtos.Responses;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MpesaSDK.NET
{
    public class MpesaClient
    {
        private string _accessToken;
        private DateTime _tokenExpiresIn;
        private readonly string _consumerKey, _consumerSecret;
        private readonly bool _isSandBox;
        private string accesstokenLock = null;
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
            //lock access token to update
            //Monitor.Enter(accesstokenLock);

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
               // Monitor.Exit(accesstokenLock);
            }
        }

        private async Task<MpesaResponse> PostRequestAsync(string resourceUrl, string data)
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

                var result = new MpesaResponse();
                if (!response.IsSuccessful)
                {
                    try
                    {
                        Console.WriteLine(response.Content);
                        result.ErrorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
                        result.IsSuccess = false;
                    }
                    catch (Exception)
                    {
                        HandleRestExceptions(response, "Error sending post request");
                    }
                }
                else
                {
                    try
                    {
                        //try parsing success response
                        Console.WriteLine(response.Content);
                        result.SuccessResponse = JsonConvert.DeserializeObject<SuccessResponse>(response.Content);
                        result.IsSuccess = true;
                    }
                    catch
                    {
                        result.ErrorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
                        result.IsSuccess = false;
                    }
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

        #endregion

        #region Public methods
        public async Task<MpesaResponse> STKPush(STKPushRequest request)
        {
            return await PostRequestAsync("mpesa/stkpush/v1/processrequest", request.ToString());
        }

        public async Task<MpesaResponse> B2C(B2CRequest request)
        {
            return await PostRequestAsync("mpesa/b2c/v1/paymentrequest", request.ToString());
        }

        public async Task<MpesaResponse> B2B(B2BRequest request)
        {
            return await PostRequestAsync("mpesa/b2b/v1/paymentrequest", request.ToString());
        }

        public async Task<MpesaResponse> C2BRegisterUrl(C2BRegisterURLRequest request)
        {
            return await PostRequestAsync("mpesa/c2b/v1/registerurl", request.ToString());
        }

        public async Task<MpesaResponse> C2BSimulateTransaction(C2BSimulateTransactionRequest request)
        {
            return await PostRequestAsync("mpesa/c2b/v1/simulate", request.ToString());
        }

        public async Task<MpesaResponse> StkPushQuery(StkPushQueryRequest request)
        {
            return await PostRequestAsync("mpesa/stkpushquery/v1/query", request.ToString());
        }

        public async Task<MpesaResponse> AccountBalance(AccountBalanceRequest request)
        {
            return await PostRequestAsync("mpesa/accountbalance/v1/query", request.ToString());
        }

        public async Task<MpesaResponse> TransactionStatus(TransactionStatusRequest request)
        {
            return await PostRequestAsync("mpesa/transactionstatus/v1/query", request.ToString());
        }

        public async Task<MpesaResponse> Reversal(ReversalRequest request)
        {
            return await PostRequestAsync("mpesa/reversal/v1/request", request.ToString());
        }

        #endregion
    }
}