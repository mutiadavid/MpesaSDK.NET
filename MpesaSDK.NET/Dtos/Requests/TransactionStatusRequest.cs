using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MpesaSDK.NET.Dtos.Requests
{
    public class TransactionStatusRequest
    {
        [JsonProperty("CommandID")]
        public string CommandID { get; set; }
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
        
    }
}
