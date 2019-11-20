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
        public string Timestamp { get; set; }               
        [JsonProperty("Password")]
        public string Password { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
