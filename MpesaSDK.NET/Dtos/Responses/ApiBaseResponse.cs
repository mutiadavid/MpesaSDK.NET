using Newtonsoft.Json;

namespace MpesaSDK.NET.Dtos.Responses
{
    public class ApiBaseResponse
    {
        public bool ResponseIsSuccess { get { return this.ResponseCode?.Equals("0") == true; } }

        [JsonProperty("ResponseCode")]
        public string ResponseCode { get; set; } = string.Empty;

        [JsonProperty("ResponseDescription")]
        public string ResponseDescription { get; set; } = string.Empty;
    }
}
