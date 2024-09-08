using MpesaSDK.NET.Dtos.InitiateRequests;
using MpesaSDK.NET.Dtos.Responses;
using MpesaSDK.NET.Enums;

namespace MpesaSDK.NET.Test
{
    internal class Program
    {
        private static readonly string consumerkey = "XQdPXlGU1IGd3OBEffVToqSjL8NMn925gmAJh5U3NqsiEkPJ";
        private static readonly string consumersecret = "DQKfmfGY9kwF9uy40wSr41iQfseQUAGN8kl6UcfF7V6hIpGADfH2VYIi8Oz2eTzv";
        private static readonly string businesscode = "600981"; //partyA
        private static readonly string phonenumber = "254708374149"; //partyB
        private static readonly string passkey = "bfb279f9aa9bdbcf158e97dd71a467cd2e0c893059b10f78e6b72ada1ed2c919";
        private static readonly string stkPushCallbackURL = "https://sandbox.free.beeceptor.com";
        private static readonly string validationURL = "https://sandbox.free.beeceptor.com/validation";
        private static readonly string confirmationURL = "https://sandbox.free.beeceptor.com/confirmation";
        private static readonly string businessname = "174379";//partyA name
        private static readonly string password = "174379"; //initiator password


        private static async Task Main(string[] args)
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            using HttpClient client = new() { Timeout = TimeSpan.FromSeconds(10) };
            MpesaClient mpesaClient = new MpesaClient(client, new MpesaClientOptions
            {
                ConsumerKey = consumerkey,
                ConsumerSecret = consumersecret,
            });

            bool exit = false;
            while (!exit)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("MPesa API Console Menu");
                    Console.WriteLine("----------------------");
                    Console.WriteLine("1. STK Push Example");
                    Console.WriteLine("2. STK Query Example");
                    Console.WriteLine("3. B2C Example");
                    Console.WriteLine("4. Reversal Example");
                    Console.WriteLine("5. Register URL Example");
                    Console.WriteLine("6. Simulate Customer Payment Example");
                    Console.WriteLine("7. Exit");
                    Console.WriteLine();
                    Console.Write("Select an option (1-7): ");

                    var input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            Console.WriteLine("Running STK Push Example...");
                            await StkPushExample(mpesaClient, tokenSource.Token);
                            break;

                        case "2":
                            Console.WriteLine("Running STK Query Example...");
                            await StkQueryExample(mpesaClient, tokenSource.Token);
                            break;

                        case "3":
                            Console.WriteLine("Running B2C Example...");
                            await B2CExample(mpesaClient, tokenSource.Token);
                            break;

                        case "4":
                            Console.WriteLine("Running Reversal Example...");
                            await ReversalExample(mpesaClient, tokenSource.Token);
                            break;

                        case "5":
                            Console.WriteLine("Running Register URL Example...");
                            await RegisterUrlExample(mpesaClient, tokenSource.Token);
                            break;

                        case "6":
                            Console.WriteLine("Running Simulate Customer Payment Example...");
                            await SimulateCustomerPaymentExample(mpesaClient, tokenSource.Token);
                            break;

                        case "7":
                            Console.WriteLine("Exiting...");
                            exit = true;
                            break;

                        default:
                            Console.WriteLine("Invalid option. Please select a valid number (1-7).");
                            break;
                    }

                    if (!exit)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Press any key to return to the menu...");
                        Console.ReadKey();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error! -> " + ex.Message);
                    Console.WriteLine("\n\n Press Any Key to Go BACK TO MENU");
                    Console.ReadLine();
                }

            }

        }

        private static async Task StkPushExample(MpesaClient mpesaclient, CancellationToken cancellationToken)
        {
            Console.WriteLine("Enter Mobile: 254xxxxxxxxx");
            string? consolePhone = Console.ReadLine() ?? phonenumber;
            StkPushResponse res = await mpesaclient.STKPushAsync(new InitiateSTKPush
            {
                BusinessCode = businesscode,
                PhoneNumber = consolePhone,
                Amount = 1,
                PassKey = passkey,
                CallbackUrl = stkPushCallbackURL,
                AccountReference = "1234567890",
                TransactionDesc = "Event Ticket",
                Command = C2BCommand.CustomerPayBillOnline
            }, cancellationToken);

            Console.WriteLine(res.ToString());
        }


        private static async Task StkQueryExample(MpesaClient mpesaclient, CancellationToken cancellationToken)
        {
            Console.WriteLine("ENTER Checkout Request ID: ws_xxxxxxxxxxxxxxxxx");
            string? requestID = Console.ReadLine() ?? string.Empty;
            StkPushQueryResponse result = await mpesaclient.StkPushQueryAsync(new InitiateSTKPushQuery
            {
                PassKey = passkey,
                BusinessShortCode = businesscode,
                CheckoutRequestID = requestID
            }, cancellationToken);

            Console.WriteLine(result.ToString());
        }

        private static async Task B2CExample(MpesaClient mpesaclient, CancellationToken cancellationToken)
        {
            CommonMpesaResponse result = await mpesaclient.B2CAsync(new InitiateB2C
            {
                InitiatorName = businessname,
                InitiatorPassword = password,
                Command = B2CCommand.SalaryPayment,
                Amount = 20,
                PartyA = businesscode,
                PartyB = phonenumber,
                Remarks = "remarks",
                QueueTimeOutURL = "https://timeouturl.com",
                ResultURL = "https://resulturl.com",
                Occassion = "occassion"
            }, cancellationToken);

            Console.WriteLine(result.ToString());
        }

        private static async Task ReversalExample(MpesaClient mpesaclient, CancellationToken cancellationToken)
        {
            CommonMpesaResponse result = await mpesaclient.ReversalAsync(new InitiateTransactionReversal
            {
                Initiator = businesscode,
                InitiatorPassword = password,
                ReceiverParty = businesscode,
                RecieverIdentifierType = IdentifierType.Shortcode,
                TransactionID = "ADVFR4355",
                Amount = 10,
                QueueTimeOutURL = "https://timeouturl.com",
                ResultURL = "https://resulturl.com"
            }, cancellationToken);

            Console.WriteLine(result.ToString());
        }

        private static async Task RegisterUrlExample(MpesaClient mpesaclient, CancellationToken cancellationToken)
        {
            CommonMpesaResponse result = await mpesaclient.C2BRegisterUrlAsync(new InitiateC2BRegisterUrl
            {
                ShortCode = businesscode,
                ValidationURL = validationURL,
                ConfirmationURL = confirmationURL,
                ResponseType = ResponseType.Completed
            }, cancellationToken);

            Console.WriteLine(result.ToString());
        }

        private static async Task SimulateCustomerPaymentExample(MpesaClient mpesaclient, CancellationToken cancellationToken)
        {
            CommonMpesaResponse result = await mpesaclient.C2BSimulateTransactionAsync(new InitiateC2BTransactionSimulation
            {
                Command = C2BCommand.CustomerPayBillOnline,
                Amount = 1,
                MSISDN = phonenumber,
                BillRefNumber = "1234567890",
                ShortCode = businesscode
            }, cancellationToken);

            Console.WriteLine(result.ToString());
        }
    }
}
