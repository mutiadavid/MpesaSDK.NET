using System;
using MpesaSDK.NET;
using System.Text;
using System.Threading.Tasks;

namespace ClientTest
{
    class Program
    {

        static void Main(string[] args)
        {
            stkPushAsync();
            
            Console.WriteLine("Hello World!");

            Console.ReadKey();
        }

        static async System.Threading.Tasks.Task stkPushAsync()
        {

            MpesaClient mpesaclient = new MpesaClient("RueQt1MojjqLivNkgPcejh6VFcn5rpkf", "3zvftXwm6SUPNiJe");
            //await server to run
            await Task.Delay(TimeSpan.FromSeconds(10));
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            var password = Convert.ToBase64String(Encoding.UTF8.GetBytes($"174379bfb279f9aa9bdbcf158e97dd71a467cd2e0c893059b10f78e6b72ada1ed2c919{timestamp}"));

            var data = new MpesaSDK.NET.Dtos.Requests.STKPushRequest()
            {
                BusinessShortCode = "174379",
                Password = password,
                Timestamp = timestamp,
                TransactionType = "CustomerPayBillOnline",
                Amount = 10,
                //change to ur number
                PartyA = "254704767562",
                PartyB = "174379",
                //change to ur number
                PhoneNumber = "254704767562",
                //local callback url exposed via ngrok
                //for more details on how to expose callback server check link below
                //https://dashboard.ngrok.com/get-started

                CallBackURL = "https://49ad24ef.ngrok.io/api/callback/StkPushCallback",
                AccountReference = "12345",
                TransactionDesc = "Pay for goods."
            };
           var result = await mpesaclient.STKPush(data);
            Console.WriteLine(result.ToString());
        }
    }
}

