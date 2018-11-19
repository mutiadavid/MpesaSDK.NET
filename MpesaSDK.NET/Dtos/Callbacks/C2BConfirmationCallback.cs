using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MpesaSDK.NET.Dtos.Callbacks
{
    public class C2BConfirmationCallback: C2BCallback
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
