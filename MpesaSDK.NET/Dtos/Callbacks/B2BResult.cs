using Newtonsoft.Json;

namespace MpesaSDK.NET.Dtos.Callbacks
{
    public class B2BResult : BaseResult
    {
        [JsonProperty("ReferenceData")]
        public B2BReferenceData ReferenceData { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
