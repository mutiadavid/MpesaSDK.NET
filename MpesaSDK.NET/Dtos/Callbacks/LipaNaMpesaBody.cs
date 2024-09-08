using Newtonsoft.Json;

namespace MpesaSDK.NET.Dtos.Callbacks
{
    public class LipaNaMpesaBody
    {
        [JsonProperty("StkCallback")]
        public StkCallback StkCallback { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
