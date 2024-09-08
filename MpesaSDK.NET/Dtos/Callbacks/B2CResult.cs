using Newtonsoft.Json;

namespace MpesaSDK.NET.Dtos.Callbacks
{
    public class B2CResult : BaseResult
    {
        [JsonProperty("ReferenceData")]
        public ReferenceData ReferenceData { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
