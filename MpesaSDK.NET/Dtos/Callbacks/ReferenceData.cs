using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MpesaSDK.NET.Dtos.Callbacks
{
    public class ReferenceData
    {
        [JsonProperty("ReferenceItem")]
        public ReferenceItem ReferenceItem { get; set; }
    }

    public class B2BReferenceData
    {
        [JsonProperty("ReferenceItem")]
        public List<ReferenceItem> ReferenceItem { get; set; }
    }
}
