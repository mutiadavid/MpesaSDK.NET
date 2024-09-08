using Newtonsoft.Json;

namespace MpesaSDK.NET.Dtos.Callbacks
{
    public class AccountBalanceCallback : B2CResult
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
