using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MpesaSDK.NET.Dtos.Callbacks
{
    public class LipaNaMpesaBody
    {
        [JsonProperty("StkCallback")]
        public StkCallback StkCallback { get; set; }
    }
}
