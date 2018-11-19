using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MpesaSDK.NET.Dtos.Requests
{
    public class AccountBalanceRequest
    {
        [JsonProperty("CommandID")]
        public string CommandID { get; set; }
        [JsonProperty("PartyB")]
        public string PartyB { get; set; }
        [JsonProperty("ReceiverIdentifierType")]
        public string ReceiverIdentifierType { get; set; }
        [JsonProperty("Remarks")]
        public string Remarks { get; set; }
        [JsonProperty("AccountType")]
        public string AccountType { get; set; }
        [JsonProperty("Initiator")]
        public string Initiator { get; set; }
        [JsonProperty("SecurityCredential")]
        public string SecurityCredential { get; set; }
        [JsonProperty("QueueTimeOutURL")]
        public string QueueTimeOutURL { get; set; }
        [JsonProperty("ResultURL")]
        public string ResultURL { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
