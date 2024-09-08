using System;

namespace MpesaSDK.NET.Validators
{
    internal static class LengthValidator
    {
        internal static void ValidateLength(string strToValidate, string fieldName, int maxlength, int minLength = 0, string errorMessage = "")
        {
            var length = strToValidate.Trim().Length;
            if (string.IsNullOrWhiteSpace(strToValidate) || length > maxlength || length < minLength)
                throw new Exception($"Invalid '{fieldName}' length. {errorMessage}");
        }
    }
}
