using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MpesaSDK.NET.Dtos.Callbacks
{
    public class ReversalCallback : B2CCallback
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
