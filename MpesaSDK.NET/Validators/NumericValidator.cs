using System;
using System.Collections.Generic;
using System.Text;

namespace MpesaSDK.NET.Validators
{
    internal static class NumericValidator
    {
        internal static void ValidateIsNumeric(string strToValidate,string fieldName,string errorMessage = "")
        {
            try
            {
                Int64.Parse(strToValidate);
                return;
            }
            catch (Exception)
            {
                throw new Exception($"{fieldName} is not a valid numeric. {errorMessage}");
            }
        }
    }
}
