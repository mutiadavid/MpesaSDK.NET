using Newtonsoft.Json;

namespace MpesaSDK.NET.Dtos.Requests
{
    public class B2CRequest
    {
        [JsonProperty("InitiatorName")]
        public string InitiatorName { get; set; }

        [JsonProperty("SecurityCredential")]
        public string SecurityCredential { get; set; }

        [JsonProperty("CommandID")]
        public string CommandID { get; set; }

        [JsonProperty("Amount")]
        public long Amount { get; set; }

        [JsonProperty("PartyA")]
        public string PartyA { get; set; }

        [JsonProperty("PartyB")]
        public string PartyB { get; set; }

        [JsonProperty("Remarks")]
        public string Remarks { get; set; }

        [JsonProperty("QueueTimeOutURL")]
        public string QueueTimeOutURL { get; set; }

        [JsonProperty("ResultURL")]
        public string ResultURL { get; set; }

        [JsonProperty("Occasion")]
        public string Occasion { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
