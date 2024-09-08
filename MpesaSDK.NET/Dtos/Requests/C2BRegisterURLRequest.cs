using Newtonsoft.Json;

namespace MpesaSDK.NET.Dtos.Requests
{
    public class C2BRegisterURLRequest
    {
        [JsonProperty("ValidationURL")]
        public string ValidationURL { get; set; }

        [JsonProperty("ConfirmationURL")]
        public string ConfirmationURL { get; set; }

        [JsonProperty("ShortCode")]
        public string ShortCode { get; set; }

        [JsonProperty("ResponseType")]
        public string ResponseType { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
