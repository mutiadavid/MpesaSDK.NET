using Newtonsoft.Json;

namespace MpesaSDK.NET.Dtos.Requests
{
    public class TransactionStatusRequest
    {
        [JsonProperty("CommandID")]
        public string CommandID { get; set; }

        [JsonProperty("PartyA")]
        public string PartyA { get; set; }

        [JsonProperty("ShortCode")]
        public string ShortCode { get; set; }

        [JsonProperty("IdentifierType")]
        public string IdentifierType { get; set; }

        [JsonProperty("Remarks")]
        public string Remarks { get; set; }

        [JsonProperty("Initiator")]
        public string Initiator { get; set; }

        [JsonProperty("SecurityCredential")]
        public string SecurityCredential { get; set; }

        [JsonProperty("QueueTimeOutURL")]
        public string QueueTimeOutURL { get; set; }

        [JsonProperty("ResultURL")]
        public string ResultURL { get; set; }

        [JsonProperty("TransactionID")]
        public string TransactionID { get; set; }

        [JsonProperty("Occasion")]
        public string Occasion { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
