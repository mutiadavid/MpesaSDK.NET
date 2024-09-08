using Newtonsoft.Json;

namespace MpesaSDK.NET.Dtos.Responses
{
    public class StkPushQueryResponse : ApiBaseResponse
    {
        [JsonProperty("MerchantRequestID")]
        public string MerchantRequestID { get; set; } = string.Empty;

        [JsonProperty("CheckoutRequestID")]
        public string CheckoutRequestID { get; set; } = string.Empty;

        [JsonProperty("ResultCode")]
        public string ResultCode { get; set; }

        [JsonProperty("ResultDesc")]
        public string ResultDesc { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
