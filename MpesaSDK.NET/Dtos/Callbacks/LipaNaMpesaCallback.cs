using Newtonsoft.Json;

namespace MpesaSDK.NET.Dtos.Callbacks
{
    public class LipaNaMpesaCallback
    {
        [JsonProperty("Body")]
        public LipaNaMpesaBody Body { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
