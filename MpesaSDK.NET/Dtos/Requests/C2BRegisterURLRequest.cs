using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MpesaSDK.NET.Dtos.Requests
{
    public class C2BRegisterURLRequest
    {
        [JsonProperty("ValidationURL")]
        public string ValidationURL { get; set; }
        [JsonProperty("ConfirmationURL")]
        public string ConfirmationURL { get; set; }
        [JsonProperty("ShortCode")]
        public string ShortCode { get; set; }
        [JsonProperty("ResponseType")]
        public string ResponseType { get; set; }
    }
}
