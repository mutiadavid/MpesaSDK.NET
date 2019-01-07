using System;
using System.Collections.Generic;
using System.Text;

namespace MpesaSDK.NET.Validators
{
    public static class PhoneNumberValidator
    {
        public static void ValidatePhoneNumber(this MpesaClient mpesaClient, string phoneNummber)
        {
            LengthValidator.ValidateLength(phoneNummber, "PhoneNumber", 12);
            if (!phoneNummber.StartsWith("2547"))
                throw new Exception("Invalid phone number");
        }
    }
}
