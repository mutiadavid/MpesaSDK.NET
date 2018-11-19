using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MpesaSDK.NET.Dtos.Callbacks
{
    public class ResultParameter
    {
        [JsonProperty("Key")]
        public string Key { get; set; }
        [JsonProperty("Value")]
        public string Value { get; set; }
    }

    public class ResultParameters
    {
        [JsonProperty("ResultParameter")]
        public List<ResultParameter> ResultParameter { get; set; }
    }

    public class LipaNaMPesaCallbackMetadata
    {
        [JsonProperty("Item")]
        public List<ResultParameter> ResultParameter { get; set; }
    }

    //public class LipaNaMPesaCallbackMetadataItem
    //{
    //    public List<ResultParameter> ResultParameter { get; set; }
    //}

}
