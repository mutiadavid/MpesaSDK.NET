using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MpesaSDK.NET.Dtos.Requests
{
    public class B2BRequest
    {
        [JsonProperty("Initiator")]
        public string Initiator { get; set; }
        [JsonProperty("SecurityCredential")]
        public string SecurityCredential { get; set; }
        [JsonProperty("CommandID")]
        public string CommandID { get; set; }
        [JsonProperty("Amount")]
        public long Amount { get; set; }
        [JsonProperty("PartyA")]
        public string PartyA { get; set; }
        [JsonProperty("SenderIdentifier")]
        public string SenderIdentifier { get; set; }
        [JsonProperty("PartyB")]
        public string PartyB { get; set; }
        [JsonProperty("RecieverIdentifierType")]
        public string RecieverIdentifierType { get; set; }
        [JsonProperty("Remarks")]
        public string Remarks { get; set; }
        [JsonProperty("QueueTimeOutURL")]
        public string QueueTimeOutURL { get; set; }
        [JsonProperty("ResultURL")]
        public string ResultURL { get; set; }
        [JsonProperty("Occasion")]
        public string Occasion { get; set; }
        [JsonProperty("AccountReference")]
        public string AccountReference { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
