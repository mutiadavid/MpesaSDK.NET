namespace MpesaSDK.NET.Dtos.Responses
{
    public class ApiErrorResponse
    {
        /// <summary>
        /// Unique identifier for the request.
        /// </summary>
        public string RequestId { get; set; } = string.Empty;

        /// <summary>
        /// Error code indicating the type of error.
        /// </summary>
        public string ErrorCode { get; set; } = string.Empty;

        /// <summary>
        /// Description of the error that occurred.
        /// </summary>
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
