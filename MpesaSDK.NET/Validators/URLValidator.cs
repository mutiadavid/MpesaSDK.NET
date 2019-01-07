using System;
using System.Collections.Generic;
using System.Text;

namespace MpesaSDK.NET.Validators
{
    internal static class URLValidator
    {
        internal static void ValidateURL(string url,string fieldName)
        {
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                throw new Exception($"{fieldName} if not a valid url");
        }
    }
}
