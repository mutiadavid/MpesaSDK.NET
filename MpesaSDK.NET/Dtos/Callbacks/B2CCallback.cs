using Newtonsoft.Json;

namespace MpesaSDK.NET.Dtos.Callbacks
{
    public class B2CCallback
    {
        [JsonProperty("Result")]
        public B2CResult Result { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
