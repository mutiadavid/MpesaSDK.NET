using Newtonsoft.Json;

namespace MpesaSDK.NET.Dtos.Callbacks
{
    public class StkCallback
    {
        [JsonProperty("MerchantRequestID")]
        public string MerchantRequestID { get; set; }

        [JsonProperty("CheckoutRequestID")]
        public string CheckoutRequestID { get; set; }

        [JsonProperty("ResultCode")]
        public string ResultCode { get; set; }

        [JsonProperty("ResultDesc")]
        public string ResultDesc { get; set; }

        [JsonProperty("CallbackMetadata")]
        public LipaNaMPesaCallbackMetadata LipaNaMPesaCallbackMetadata { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
