using System;

namespace MpesaSDK.NET.Validators
{
    public static class PhoneNumberValidator
    {
        public static void ValidatePhoneNumber(this MpesaClient mpesaClient, string phoneNummber)
        {
            LengthValidator.ValidateLength(phoneNummber, "PhoneNumber", 12);
            if (!phoneNummber.StartsWith("254"))
                throw new Exception("Invalid phone number");
        }
    }
}
