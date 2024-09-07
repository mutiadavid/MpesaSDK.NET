namespace MpesaSDK.NET.Test
{
    internal class Program
    {
        private static string consumerkey = "XQdPXlGU1IGd3OBEffVToqSjL8NMn925gmAJh5U3NqsiEkPJ";
        private static string consumersecret = "DQKfmfGY9kwF9uy40wSr41iQfseQUAGN8kl6UcfF7V6hIpGADfH2VYIi8Oz2eTzv";
        private static string businesscode = "174379"; //partyA
        private static string phonenumber = "254708374149"; //partyB
        private static string passkey = "bfb279f9aa9bdbcf158e97dd71a467cd2e0c893059b10f78e6b72ada1ed2c919";
        private static string stkPushCallbackURL = "https://sandbox.free.beeceptor.com";
        private static string validationURL = "https://sandbox.free.beeceptor.com/validation";
        private static string confirmationURL = "https://sandbox.free.beeceptor.com/confirmation";
        public static string businessname = "174379";//partyA name
        public static string password = "174379"; //initiator password

        static MpesaClient mpesaclient = new MpesaClient(consumerkey, consumersecret);

        private static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Started!");

                // await StkPushExample();

                //await StkQueryExample();

                //await B2CExample();

                //await ReversalExample();

                // await RegisterUrlExample();

                await SimulateCustomerPaymentExample();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error! -> " + ex.Message);
            }


            Console.ReadKey();
        }

        private static async Task StkPushExample()
        {
            var res = await mpesaclient.STKPushAsync(businesscode, phonenumber, 1, passkey, stkPushCallbackURL, accountReference: "1234567890", transactionDesc: "Event Ticket");

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

        private static async Task RegisterUrlExample()
        {
            var result = await mpesaclient.C2BRegisterUrlAsync(validationURL, confirmationURL, Enums.ResponseType.Completed, businesscode);
            Console.WriteLine(result.ToString());
        }

        private static async Task SimulateCustomerPaymentExample()
        {
            var result = await mpesaclient.C2BSimulateTransactionAsync(Enums.Command.CustomerPayBillOnline, 1, phonenumber, "1234567890", businesscode);
            Console.WriteLine(result.ToString());
        }
    }
}
