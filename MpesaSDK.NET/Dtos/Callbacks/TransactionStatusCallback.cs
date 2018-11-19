using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MpesaSDK.NET.Dtos.Callbacks
{
    public class TransactionStatusCallback: B2BCallback
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
