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
        [JsonProperty("ReceiverParty")]
        public string ReceiverParty { get; set; }
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
        [JsonProperty("Amount")]
        public long Amount { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
