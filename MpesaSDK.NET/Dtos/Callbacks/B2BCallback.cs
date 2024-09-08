﻿using Newtonsoft.Json;

namespace MpesaSDK.NET.Dtos.Callbacks
{
    public class B2BCallback
    {
        [JsonProperty("Result")]
        public B2BResult Result { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
