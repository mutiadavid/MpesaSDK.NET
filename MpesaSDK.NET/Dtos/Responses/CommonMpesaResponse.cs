using Newtonsoft.Json;

namespace MpesaSDK.NET.Dtos.Responses
{
    public class CommonMpesaResponse
    {
        [JsonProperty("ConversationID")]
        public string ConversationID { get; set; }

        [JsonProperty("OriginatorConversationID")]
        public string OriginatorConversationID { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
