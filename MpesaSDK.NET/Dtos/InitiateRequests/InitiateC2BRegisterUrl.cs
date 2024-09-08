using MpesaSDK.NET.Enums;

namespace MpesaSDK.NET.Dtos.InitiateRequests
{
    public class InitiateC2BRegisterUrl
    {
        /// <summary>
        /// The URL that receives the validation request from the API upon payment submission. 
        /// This URL is only called if the external validation on the registered shortcode is enabled.
        /// (By default, External Validation is disabled.)
        /// </summary>
        public string ValidationURL { get; set; }

        /// <summary>
        /// The URL that receives the confirmation request from the API upon payment completion.
        /// </summary>
        public string ConfirmationURL { get; set; }

        /// <summary>
        /// Specifies what happens if the validation URL is not reachable. 
        /// Allowed values are:
        /// - Completed: M-Pesa will automatically complete your transaction.
        /// - Cancelled: M-Pesa will automatically cancel the transaction if the validation URL is unreachable.
        /// </summary>
        public ResponseType ResponseType { get; set; } = ResponseType.Completed;

        /// <summary>
        /// The short code of the organization.
        /// </summary>
        public string ShortCode { get; set; }
    }
}
