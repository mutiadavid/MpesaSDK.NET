using Newtonsoft.Json;

namespace MpesaSDK.NET.Dtos.Callbacks
{
    public class ReversalCallback : B2CCallback
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
