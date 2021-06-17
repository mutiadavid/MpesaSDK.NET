using System;
using System.Collections.Generic;
using System.Text;

namespace MpesaSDK.NET.Validators
{
    public static class BusinessShortCodeValidator
    {
        public static void ValidateBusinessShortCode(this MpesaClient mpesaClient, string code)
        {
            NumericValidator.ValidateIsNumeric(code, "BusinessShortCode");
            LengthValidator.ValidateLength(code, "BusinessShortCode", 13, 5);
        }
    }
}
