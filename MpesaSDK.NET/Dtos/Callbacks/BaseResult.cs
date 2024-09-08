using Newtonsoft.Json;

namespace MpesaSDK.NET.Dtos.Callbacks
{
    public class BaseResult
    {
        [JsonProperty("ResultType")]
        public long ResultType { get; set; }

        [JsonProperty("ResultCode")]
        public long ResultCode { get; set; }

        [JsonProperty("ResultDesc")]
        public string ResultDesc { get; set; }

        [JsonProperty("OriginatorConversationID")]
        public string OriginatorConversationID { get; set; }

        [JsonProperty("ConversationID")]
        public string ConversationID { get; set; }

        [JsonProperty("TransactionID")]
        public string TransactionID { get; set; }

        [JsonProperty("ResultParameters")]
        public ResultParameters ResultParameters { get; set; }
    }
}
