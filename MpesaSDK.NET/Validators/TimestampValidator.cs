using System;
using System.Collections.Generic;
using System.Text;

namespace MpesaSDK.NET.Validators
{
    public static class TimestampValidator
    {
        public static void ValidateTimestamp(this MpesaClient mpesaClient,string timestamp)
        {
            if(timestamp.Length == "YYYYMMDDHHmmss".Length)
            {
                try
                {
                    DateTime.Parse(timestamp);
                    return;
                }
                catch (Exception)
                {
                }
            }

            throw new Exception("Invalid timestamp");
        }
    }
}
