using Newtonsoft.Json;

namespace MpesaSDK.NET.Dtos.Callbacks
{
    public class C2BValidationCallback : C2BCallback
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
