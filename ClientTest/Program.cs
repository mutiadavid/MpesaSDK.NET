using MpesaSDK.NET;
using MpesaSDK.NET.Dtos.Requests;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ClientTest
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Started!");

                MpesaClient mpesaclient = new MpesaClient("RueQt1MojjqLivNkgPcejh6VFcn5rpkf", "3zvftXwm6SUPNiJe");

                var res = await mpesaclient.STKPush("174379", "254704767562", 10, "bfb279f9aa9bdbcf158e97dd71a467cd2e0c893059b10f78e6b72ada1ed2c919", "https://urdomain.ext/path", accountReference: "1234567890", transactionDesc: "Goods payment");

                Console.WriteLine(res.ToString());


                //var regRes = await mpesaclient.C2BRegisterUrl(new C2BRegisterURLRequest()
                //{
                //    ValidationURL = "https://9514805e.ngrok.io/api/Callback/C2BValidation",
                //    ConfirmationURL = "https://9514805e.ngrok.io/api/Callback/C2BConfirmation",
                //    //174379
                //    //600000
                //    //601497
                //    ShortCode = "601497",
                //    ResponseType = "Completed"
                //});

                //Console.WriteLine(regRes.ToString());

                //var simulateC2B = await mpesaclient.C2BSimulateTransaction(new C2BSimulateTransactionRequest()
                //{
                //    Amount = 10,
                //    BillRefNumber = "",
                //    CommandID = "CustomerBuyGoodsOnline",
                //    ShortCode = "600000",
                //    MSISDN = "254704767562"
                //});



                //Console.WriteLine(simulateC2B.ToString());


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error! -> " + ex.Message);
            }


            Console.ReadKey();
        }

        //static async System.Threading.Tasks.Task stkPushAsync()
        //{
        //    try
        //    {
        //       //await server to run
        //        //await Task.Delay(TimeSpan.FromSeconds(10));
        //        var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
        //        var password = Convert.ToBase64String(Encoding.UTF8.GetBytes($"174379bfb279f9aa9bdbcf158e97dd71a467cd2e0c893059b10f78e6b72ada1ed2c919{timestamp}"));

        //        var data = new MpesaSDK.NET.Dtos.Requests.STKPushRequest()
        //        {
        //            BusinessShortCode = "174379",
        //            Password = password,
        //            Timestamp = timestamp,
        //            TransactionType = "CustomerPayBillOnline",
        //            Amount = 10,
        //            //change to ur number
        //            PartyA = "254704767562",
        //            PartyB = "174379",
        //            //change to ur number
        //            PhoneNumber = "254704767562",
        //            //local callback url exposed via ngrok
        //            //for more details on how to expose callback server check link below
        //            //https://dashboard.ngrok.com/get-started

        //            CallBackURL = "49ad24ef.ngrok.io/api/callback/StkPushCallback",
        //            AccountReference = "12345",
        //            TransactionDesc = "Pay for ads"
        //        };
        //        var result = await mpesaclient.STKPush(data);
        //        Console.WriteLine(result.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }


        //}



        private static async Task stkQuery()
        {
            //ws_CO_DMZ_155474528_19112018171250687
            MpesaClient mpesaclient = new MpesaClient("RueQt1MojjqLivNkgPcejh6VFcn5rpkf", "3zvftXwm6SUPNiJe");
            //await server to run
            //await Task.Delay(TimeSpan.FromSeconds(10));
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            var password = Convert.ToBase64String(Encoding.UTF8.GetBytes($"174379bfb279f9aa9bdbcf158e97dd71a467cd2e0c893059b10f78e6b72ada1ed2c919{timestamp}"));

            var result = await mpesaclient.StkPushQuery(new MpesaSDK.NET.Dtos.Requests.StkPushQueryRequest
            {
                BusinessShortCode = "174379",
                CheckoutRequestID = "ws_CO_DMZ_155474528_19112018171250687",
                Password = password,
                Timestamp = timestamp
            });

            Console.WriteLine(result.ToString());
        }
    }
}

