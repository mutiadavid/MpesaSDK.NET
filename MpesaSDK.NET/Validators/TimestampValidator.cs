using System;
using System.Diagnostics;

namespace MpesaSDK.NET.Validators
{
    public static class TimestampValidator
    {
        public static void ValidateTimestamp(this MpesaClient mpesaClient, string timestamp)
        {
            if (timestamp.Length == "YYYYMMDDHHmmss".Length)
            {
                try
                {
                    DateTime.ParseExact(timestamp, "yyyyMMddHHmmss", provider: System.Globalization.CultureInfo.InvariantCulture);
                    return;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
            throw new Exception("Invalid timestamp");
        }
    }
}
