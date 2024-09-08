using Newtonsoft.Json;

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

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
