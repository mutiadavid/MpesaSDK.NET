using Newtonsoft.Json;

namespace MpesaSDK.NET.Dtos.Responses
{
    public class StkPushResponse
    {
        [JsonProperty("IsSuccess")]
        public bool IsSuccess { get; set; }
        [JsonProperty("SuccessResponse")]
        public StkPushSuccessResponse SuccessResponse { get; set; }
        [JsonProperty("ErrorResponse")]
        public ErrorResponse ErrorResponse { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
