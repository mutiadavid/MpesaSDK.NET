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
                    DateTime.ParseExact(timestamp, "yyyyMMddHHmmss", provider: System.Globalization.CultureInfo.InvariantCulture);
                    return;
                }
                catch (Exception ex)
                {
                }
            }

            throw new Exception("Invalid timestamp");
        }
    }
}
