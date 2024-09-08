using Newtonsoft.Json;

namespace MpesaSDK.NET.Dtos.Callbacks
{
    public class ReferenceItem
    {
        [JsonProperty("Key")]
        public string Key { get; set; }

        [JsonProperty("Value")]
        public string Value { get; set; }
    }
}
