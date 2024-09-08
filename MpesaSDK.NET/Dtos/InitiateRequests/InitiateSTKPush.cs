using MpesaSDK.NET.Enums;

namespace MpesaSDK.NET.Dtos.InitiateRequests
{
    public class InitiateSTKPush
    {
        public string BusinessCode { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public long Amount { get; set; }
        public string PassKey { get; set; } = string.Empty;
        public string CallbackUrl { get; set; } = string.Empty;
        public string AccountReference { get; set; } = string.Empty;
        public string TransactionDesc { get; set; } = string.Empty;
        public C2BCommand Command { get; set; } = C2BCommand.CustomerPayBillOnline;
    }
}
