using Newtonsoft.Json;

namespace MpesaSDK.NET.Dtos.Callbacks
{
    public class TransactionStatusCallback : B2BCallback
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
