using System;
using System.Collections.Generic;
using System.Text;

namespace MpesaSDK.NET.Validators
{
    internal static class SameValueValidator
    {
        internal static void ValidateSameValue(string a, string b, string aFieldName,string bFielldName, string errorMessage = "")
        {
            if (a != b)
                throw new Exception($"{aFieldName} != {bFielldName}. {errorMessage}");
        }
    }
}
