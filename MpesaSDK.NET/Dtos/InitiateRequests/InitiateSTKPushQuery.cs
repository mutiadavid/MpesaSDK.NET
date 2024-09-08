namespace MpesaSDK.NET.Dtos.InitiateRequests
{
    public class InitiateSTKPushQuery
    {
        public string BusinessShortCode { get; set; } = string.Empty;
        public string CheckoutRequestID { get; set; } = string.Empty;
        public string PassKey { get; set; } = string.Empty;
    }
}
