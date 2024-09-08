using MpesaSDK.NET.Dtos.InitiateRequests;
using MpesaSDK.NET.Dtos.Requests;
using MpesaSDK.NET.Dtos.Responses;
using MpesaSDK.NET.Enums;
using MpesaSDK.NET.Extensions;
using MpesaSDK.NET.Validators;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MpesaSDK.NET
{
    public class MpesaClient
    {
        private readonly HttpClient _httpClient;
        private readonly MpesaClientOptions _options;

        public MpesaClient(HttpClient httpClient, MpesaClientOptions options)
        {
            this._options = options;
            this._httpClient = httpClient;
            this._httpClient.BaseAddress = options.IsSandBox ? new Uri("https://sandbox.safaricom.co.ke") : new Uri("https://api.safaricom.co.ke");
        }

        /// <summary>
        /// Use this API to initiate online payment on behalf of a customer.
        /// </summary>
        public async Task<StkPushResponse> STKPushAsync(InitiateSTKPush initiateRequest, CancellationToken cancellationToken)
        {
            initiateRequest.PhoneNumber = HelperFunctions.CorrectPhoneNumber(initiateRequest.PhoneNumber);
            (string password, string timestamp) = HelperFunctions.MpesaPassword(initiateRequest.PassKey, initiateRequest.BusinessCode);
            STKPushRequest apiRequest = new STKPushRequest()
            {
                BusinessShortCode = initiateRequest.BusinessCode,
                PartyA = initiateRequest.PhoneNumber,
                PartyB = initiateRequest.BusinessCode,
                PhoneNumber = initiateRequest.PhoneNumber,
                TransactionType = initiateRequest.Command.ToString(),
                TransactionDesc = initiateRequest.TransactionDesc,
                Amount = initiateRequest.Amount,
                CallBackURL = initiateRequest.CallbackUrl,
                AccountReference = initiateRequest.AccountReference,
                Password = password,
                Timestamp = timestamp
            };

            //validations
            this.ValidatePhoneNumber(apiRequest.PhoneNumber);
            SameValueValidator.ValidateSameValue(apiRequest.PhoneNumber, apiRequest.PartyA, "PatyA", "PhoneNumber");
            this.ValidateTimestamp(apiRequest.Timestamp);
            this.ValidateBusinessShortCode(apiRequest.BusinessShortCode);

            SameValueValidator.ValidateSameValue(apiRequest.PartyB, apiRequest.BusinessShortCode, "PartyB", "BusinessShortCode");
            LengthValidator.ValidateLength(apiRequest.AccountReference, "AccountReference", 12);
            LengthValidator.ValidateLength(apiRequest.TransactionDesc, "TransactionDesc", 13, 1);
            URLValidator.ValidateURL(apiRequest.CallBackURL, "CallBackURL");

            return await _httpClient.PostJsonAsync<StkPushResponse>("mpesa/stkpush/v1/processrequest", apiRequest, this._options, cancellationToken);
        }

        /// <summary>
        /// Use this API to check the status of a Lipa Na M-Pesa Online Payment
        /// </summary>
        public async Task<StkPushQueryResponse> StkPushQueryAsync(InitiateSTKPushQuery initiateRequest, CancellationToken cancellationToken)
        {
            (string password, string timestamp) = HelperFunctions.MpesaPassword(initiateRequest.PassKey, initiateRequest.BusinessShortCode);
            StkPushQueryRequest apiRequest = new StkPushQueryRequest()
            {
                BusinessShortCode = initiateRequest.BusinessShortCode,
                CheckoutRequestID = initiateRequest.CheckoutRequestID,
                Password = password,
                Timestamp = timestamp
            };

            return await _httpClient.PostJsonAsync<StkPushQueryResponse>("mpesa/stkpushquery/v1/query", apiRequest, this._options, cancellationToken);
        }

        /// <summary>
        /// Use this API to transact between an M-Pesa short code to a phone number registered on M-Pesa.
        /// </summary>
        public async Task<CommonMpesaResponse> B2CAsync(InitiateB2C initiateRequest, CancellationToken cancellationToken)
        {
            B2CRequest apiRequest = new B2CRequest()
            {
                InitiatorName = initiateRequest.InitiatorName,
                SecurityCredential = initiateRequest.InitiatorPassword.ToMpesaSecurityCredential(),
                CommandID = initiateRequest.Command.ToString(),
                Amount = initiateRequest.Amount,
                Occasion = initiateRequest.Occassion,
                PartyA = initiateRequest.PartyA,
                PartyB = initiateRequest.PartyB,
                Remarks = initiateRequest.Remarks,
                ResultURL = initiateRequest.ResultURL,
                QueueTimeOutURL = initiateRequest.QueueTimeOutURL
            };

            return await _httpClient.PostJsonAsync<CommonMpesaResponse>("mpesa/b2c/v1/paymentrequest", apiRequest, this._options, cancellationToken);
        }

        /// <summary>
        /// This API enables Business to Business (B2B) transactions between a business and another business. Use of this API requires a valid and verified B2B M-Pesa short code for the business initiating the transaction and the both businesses involved in the transaction.
        /// </summary>
        public async Task<CommonMpesaResponse> B2BAsync(InitiateB2B initiateRequest, CancellationToken cancellationToken)
        {
            B2BRequest apiRequest = new B2BRequest
            {
                Initiator = initiateRequest.Initiator,
                PartyA = initiateRequest.PartyA,
                PartyB = initiateRequest.PartyB,
                CommandID = initiateRequest.Command.ToString(),
                Amount = initiateRequest.Amount,
                AccountReference = initiateRequest.AccountReference,
                SecurityCredential = initiateRequest.InitiatorPassword.ToMpesaSecurityCredential(),
                Remarks = initiateRequest.Remarks,
                QueueTimeOutURL = initiateRequest.QueueTimeOutURL,
                ResultURL = initiateRequest.ResultURL,
                RecieverIdentifierType = initiateRequest.ReceiverIdentifierType.ToString("d"),
                SenderIdentifier = initiateRequest.SenderIdentifier.ToString("d")
            };

            return await _httpClient.PostJsonAsync<CommonMpesaResponse>("mpesa/b2b/v1/paymentrequest", apiRequest, this._options, cancellationToken);
        }

        /// <summary>
        /// Use this API to register validation and confirmation URLs on M-Pesa 
        /// </summary>
        public async Task<CommonMpesaResponse> C2BRegisterUrlAsync(InitiateC2BRegisterUrl initiateRequest, CancellationToken cancellationToken)
        {
            C2BRegisterURLRequest apiRequest = new C2BRegisterURLRequest()
            {
                ValidationURL = initiateRequest.ValidationURL,
                ConfirmationURL = initiateRequest.ConfirmationURL,
                ShortCode = initiateRequest.ShortCode,
                ResponseType = initiateRequest.ResponseType.ToString()
            };

            return await _httpClient.PostJsonAsync<CommonMpesaResponse>("mpesa/c2b/v1/registerurl", apiRequest, this._options, cancellationToken);
        }

        /// <summary>
        /// This API is used to make payment requests from Client to Business (C2B). You can use the sandbox provided test credentials down below to simulates a payment made from the client phone's STK/SIM Toolkit menu, and enables you to receive the payment requests in real time. 
        /// </summary>
        public async Task<CommonMpesaResponse> C2BSimulateTransactionAsync(InitiateC2BTransactionSimulation initiateRequest, CancellationToken cancellationToken)
        {
            C2BSimulateTransactionRequest apiRequest = new C2BSimulateTransactionRequest()
            {
                CommandID = initiateRequest.Command.ToString(),
                MSISDN = initiateRequest.MSISDN,
                Amount = initiateRequest.Amount,
                BillRefNumber = initiateRequest.BillRefNumber,
                ShortCode = initiateRequest.ShortCode
            };

            return await _httpClient.PostJsonAsync<CommonMpesaResponse>("mpesa/c2b/v1/simulate", apiRequest, this._options, cancellationToken);
        }

        /// <summary>
        /// Use this API to enquire the balance on an M-Pesa BuyGoods (Till Number).
        /// </summary>
        public async Task<CommonMpesaResponse> AccountBalanceAsync(InitiateAccountBalCheck initiateRequest, CancellationToken cancellationToken)
        {
            AccountBalanceRequest apiRequest = new AccountBalanceRequest()
            {
                CommandID = QueryCommand.AccountBalance.ToString(),
                IdentifierType = initiateRequest.IdentifierType.ToString("d"),
                PartyA = initiateRequest.PartyA,
                Initiator = initiateRequest.Initiator,
                SecurityCredential = initiateRequest.InitiatorPassword.ToMpesaSecurityCredential(),
                Remarks = initiateRequest.Remarks,
                ResultURL = initiateRequest.ResultURL,
                QueueTimeOutURL = initiateRequest.QueueTimeOutURL
            };

            return await _httpClient.PostJsonAsync<CommonMpesaResponse>("mpesa/accountbalance/v1/query", apiRequest, this._options, cancellationToken);
        }

        /// <summary>
        /// Query Transaction Status
        /// </summary>
        public async Task<CommonMpesaResponse> TransactionStatusAsync(InitiateTransactionStatusCheck initiateRequest, CancellationToken cancellationToken)
        {
            TransactionStatusRequest apiRequest = new TransactionStatusRequest()
            {
                CommandID = QueryCommand.TransactionStatusQuery.ToString(),
                PartyA = initiateRequest.PartyA,
                ShortCode = initiateRequest.PartyA,
                IdentifierType = initiateRequest.IdentifierType.ToString("d"),
                Initiator = initiateRequest.Initiator,
                SecurityCredential = initiateRequest.InitiatorPassword.ToMpesaSecurityCredential(),
                TransactionID = initiateRequest.TransactionID,
                Occasion = initiateRequest.Occasion,
                Remarks = initiateRequest.Remarks,
                ResultURL = initiateRequest.ResultURL,
                QueueTimeOutURL = initiateRequest.QueueTimeOutURL
            };

            return await _httpClient.PostJsonAsync<CommonMpesaResponse>("mpesa/transactionstatus/v1/query", apiRequest, this._options, cancellationToken);
        }

        /// <summary>
        /// Transaction Reversal API reverses a M-Pesa transaction.
        /// </summary>
        public async Task<CommonMpesaResponse> ReversalAsync(InitiateTransactionReversal initiateRequest, CancellationToken cancellationToken)
        {
            ReversalRequest apiRequest = new ReversalRequest()
            {
                CommandID = QueryCommand.TransactionReversal.ToString(),
                Initiator = initiateRequest.Initiator,
                SecurityCredential = initiateRequest.InitiatorPassword.ToMpesaSecurityCredential(),
                ReceiverParty = initiateRequest.ReceiverParty,
                RecieverIdentifierType = initiateRequest.RecieverIdentifierType.ToString(),
                TransactionID = initiateRequest.TransactionID,
                Amount = initiateRequest.Amount,
                ResultURL = initiateRequest.ResultURL,
                QueueTimeOutURL = initiateRequest.QueueTimeOutURL,
                Occasion = initiateRequest.Occasion,
                Remarks = initiateRequest.Remarks
            };

            return await _httpClient.PostJsonAsync<CommonMpesaResponse>("mpesa/reversal/v1/request", apiRequest, this._options, cancellationToken);
        }
    }
}