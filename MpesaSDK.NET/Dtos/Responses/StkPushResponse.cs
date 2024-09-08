using Newtonsoft.Json;

namespace MpesaSDK.NET.Dtos.Responses
{
    public class StkPushResponse : ApiBaseResponse
    {
        [JsonProperty("MerchantRequestID")]
        public string MerchantRequestID { get; set; } = string.Empty;

        [JsonProperty("CheckoutRequestID")]
        public string CheckoutRequestID { get; set; } = string.Empty;

        [JsonProperty("CustomerMessage")]
        public string CustomerMessage { get; set; } = string.Empty;

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
