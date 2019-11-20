using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MpesaSDK.NET.Dtos.Requests
{
    public class STKPushRequest
    {
        [JsonProperty("BusinessShortCode")]
        public string BusinessShortCode { get; set; }
        [JsonProperty("TransactionType")]
        public string TransactionType { get; set; }
        [JsonProperty("Amount")]
        public long Amount { get; set; }
        [JsonProperty("PartyA")]
        public string PartyA { get; set; }
        [JsonProperty("PartyB")]
        public string PartyB { get; set; }
        [JsonProperty("PhoneNumber")]
        public string PhoneNumber { get; set; }
        [JsonProperty("CallBackURL")]
        public string CallBackURL { get; set; }
        [JsonProperty("AccountReference")]
        public string AccountReference { get; set; }
        [JsonProperty("TransactionDesc")]
        public string TransactionDesc { get; set; }
        
        [JsonProperty("Timestamp")]
        public string Timestamp { get; set; } = DateTime.Now.ToString("yyyyMMddHHmmss");

        public string Passkey { get; set; }

        private string CalculatePassword => Convert.ToBase64String(Encoding.UTF8.GetBytes(BusinessShortCode + Passkey + Timestamp));

        [JsonProperty("Password")]
        public string Password { get => CalculatePassword; set => value = CalculatePassword; } 
        
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
