using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MpesaSDK.NET.Dtos.Callbacks
{
    public class B2BResult: BaseResult
    {
        [JsonProperty("ReferenceData")]
        public B2BReferenceData ReferenceData { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
