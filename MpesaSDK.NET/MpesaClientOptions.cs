namespace MpesaSDK.NET
{
    public class MpesaClientOptions
    {
        public bool IsSandBox { get; set; } = true;
        public string ConsumerKey { get; set; } = string.Empty;
        public string ConsumerSecret { get; set; } = string.Empty;
    }
}
