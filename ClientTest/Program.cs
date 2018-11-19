using System;
using MpesaSDK.NET;
using System.Text;

namespace ClientTest
{
    class Program
    {
        static MpesaClient mpesaclient = new MpesaClient("RueQt1MojjqLivNkgPcejh6VFcn5rpkf", "3zvftXwm6SUPNiJe");

        static void Main(string[] args)
        {
            stkPushAsync();
            
            Console.WriteLine("Hello World!");

            Console.ReadKey();
        }

        static async System.Threading.Tasks.Task stkPushAsync()
        {
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            var password = Convert.ToBase64String(Encoding.UTF8.GetBytes($"174379bfb279f9aa9bdbcf158e97dd71a467cd2e0c893059b10f78e6b72ada1ed2c919{timestamp}"));

            var data = new MpesaSDK.NET.Dtos.Requests.STKPushRequest()
            {
                BusinessShortCode = "174379",
                Password = password,
                Timestamp = timestamp,
                TransactionType = "CustomerPayBillOnline",
                Amount = 180,
                PartyA = "254704767562",
                PartyB = "174379",
                PhoneNumber = "254704767562",
                CallBackURL = "https://somesite.ke.xy",
                AccountReference = "12345",
                TransactionDesc = "Pay for goods."
            };
           var result = await mpesaclient.STKPush(data);
            Console.WriteLine(result.ToString());
        }
    }
}


//Shortcode 1	600129
//Initiator Name(Shortcode 1)    apitest361
//Security Credential(Shortcode 1)   361reset
//Shortcode 2	600000
//Test MSISDN 254708374149
//ExpiryDate	2018-11-21T23:58:05+03:00
//Lipa Na Mpesa Online Shortcode:	174379
//Lipa Na Mpesa Online Passkey:
//bfb279f9aa9bdbcf158e97dd71a467cd2e0c893059b10f78e6b72ada1ed2c919