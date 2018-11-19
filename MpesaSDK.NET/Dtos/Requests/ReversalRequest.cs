using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MpesaSDK.NET.Dtos.Requests
{
    public class ReversalRequest
    {
        [JsonProperty("Initiator")]
        public string Initiator { get; set; }
        [JsonProperty("SecurityCredential")]
        public string SecurityCredential { get; set; }
        [JsonProperty("CommandID")]
        public string CommandID { get; set; }
        [JsonProperty("PartyA")]
        public string PartyA { get; set; }
        [JsonProperty("RecieverIdentifierType")]
        public string RecieverIdentifierType { get; set; }
        [JsonProperty("Remarks")]
        public string Remarks { get; set; }
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
            return JsonConvert.SerializeObject(this);
        }
    }
}
