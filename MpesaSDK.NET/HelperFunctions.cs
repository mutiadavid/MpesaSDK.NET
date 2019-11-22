using System;
using System.Collections.Generic;
using System.Text;

namespace MpesaSDK.NET
{
    public static class HelperFunctions
    {
        public static (string password,string timestamp) MpesaPassword(string passkey, string businessCode)
        {
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            var password = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{businessCode}{passkey}{timestamp}"));

            return (password,timestamp);
        }
    }
}
