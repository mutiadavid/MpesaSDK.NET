using MpesaSDK.NET;
using MpesaSDK.NET.Dtos.Requests;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ClientTest
{
    internal class Program
    {
        private static string consumerkey = "RueQt1MojjqLivNkgPcejh6VFcn5rpkf";
        private static string consumersecret = "3zvftXwm6SUPNiJe";
        private static string businesscode = "174379"; //partyA
        private static string phonenumber = "0704767562"; //partyB
        private static string passkey = "bfb279f9aa9bdbcf158e97dd71a467cd2e0c893059b10f78e6b72ada1ed2c919";
        private static string stkPushCallbackURL = "https://urdomain.ext/path";
        public static string businessname = "174379";//partyA name
        public static string password = "174379"; //initiator password

        static MpesaClient mpesaclient = new MpesaClient(consumerkey, consumersecret);

        private static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Started!");

                await StkPushExample();

                //await StkQueryExample();

                //await B2CExample();

                //await ReversalExample();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error! -> " + ex.Message);
            }


            Console.ReadKey();
        }

        private static async Task StkPushExample()
        {
            var res = await mpesaclient.STKPushAsync(businesscode, phonenumber, 10, passkey, stkPushCallbackURL, accountReference: "1234567890", transactionDesc: "Event Ticket");

            Console.WriteLine(res.ToString());
        }


        private static async Task StkQueryExample()
        {
            var result = await mpesaclient.StkPushQueryAsync(businesscode,/*"your-checkout-request-id"*/"ws_CO_260320201332029957", passkey);
            Console.WriteLine(result.ToString());
        }

        private static async Task B2CExample()
        {
            //valid commands SalaryPayment, BusinessPayment, PromotionPayment
            //change result and timeout urls
            var result = await mpesaclient.B2CAsync(businessname, password, MpesaSDK.NET.Enums.Command.BusinessPayment, 20, businesscode, phonenumber, "remarks", "https://timeouturl.com", "https://resulturl.com", "occassion");
            Console.WriteLine(result.ToString());
        }

        private static async Task ReversalExample()
        {
            var result = await mpesaclient.ReversalAsync(businesscode, password, businesscode, MpesaSDK.NET.Enums.IdentifierType.Shortcode, "ADVFR4355", 10, "https://timeouturl.com", "https://resulturl.com");
            Console.WriteLine(result.ToString());

        }
    }
}

