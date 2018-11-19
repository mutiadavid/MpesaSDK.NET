using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MpesaSDK.NET.Dtos.Requests
{
    public class C2BSimulateTransactionRequest
    {
        [JsonProperty("CommandID")]
        public string CommandID { get; set; }
        [JsonProperty("Amount")]
        public long Amount { get; set; }
        [JsonProperty("Msisdn")]
        public string MSISDN { get; set; }
        [JsonProperty("BillRefNumber")]
        public string BillRefNumber { get; set; }
        [JsonProperty("ShortCode")]
        public string ShortCode { get; set; }
    }
}
