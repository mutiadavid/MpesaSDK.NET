using System;
using System.Text;

namespace MpesaSDK.NET
{
    /// <summary>
    /// Helper functions
    /// </summary>
    public static class HelperFunctions
    {

        /// <summary>
        /// Generate mpesa password
        /// </summary>
        /// <param name="passkey"></param>
        /// <param name="businessCode"></param>
        /// <returns></returns>
        public static (string password, string timestamp) MpesaPassword(string passkey, string businessCode)
        {
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            string password = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{businessCode}{passkey}{timestamp}"));
            return (password, timestamp);
        }

        /// <summary>
        /// Add 254 to a phone number
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static string CorrectPhoneNumber(string phone)
        {
            //valid number 254*********

            if (phone.Length == 12) return phone;

            //remove 0 and add prefix 254
            if (phone.Length == 10) return $"254{phone.Substring(1, phone.Length - 1)}";

            //add prefix 254
            if (phone.Length == 9) return $"254{phone}";

            return phone;
        }
    }
}
