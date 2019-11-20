using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MpesaSDK.NET.Dtos.Requests
{
    public class StkPushQueryRequest
    {
        [JsonProperty("BusinessShortCode")]
        public string BusinessShortCode { get; set; }
        [JsonProperty("CheckoutRequestID")]
        public string CheckoutRequestID { get; set; }
        
        [JsonProperty("Timestamp")]
        public string Timestamp { get; set; } = DateTime.Now.ToString("yyyymmddhhiiss");

        public string Passkey { get; set; }

        private string CalculatePassword => Convert.ToBase64String(Encoding.UTF8.GetBytes(PartyB + Passkey + Timestamp));

        [JsonProperty("Password")]
        public string Password { get => CalculatePassword; set => value = CalculatePassword; } 

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
