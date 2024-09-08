using System;

namespace MpesaSDK.NET.Validators
{
    internal static class URLValidator
    {
        internal static void ValidateURL(string url, string fieldName)
        {
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                throw new Exception($"{fieldName} is not a valid url");
        }
    }
}
