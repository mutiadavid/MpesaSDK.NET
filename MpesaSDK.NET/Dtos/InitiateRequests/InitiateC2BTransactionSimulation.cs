using MpesaSDK.NET.Enums;

namespace MpesaSDK.NET.Dtos.InitiateRequests
{
    public class InitiateC2BTransactionSimulation
    {

        /// <summary>
        /// The amount being transacted. This parameter is expected to be a numeric value.
        /// </summary>
        public long Amount { get; set; }

        /// <summary>
        /// The phone number initiating the C2B transaction. Should be in the format of the phone number.
        /// </summary>
        public string MSISDN { get; set; }

        /// <summary>
        /// Used with the CustomerPayBillOnline option only. This is where a customer is expected to enter a unique bill identifier, such as an Account Number.
        /// </summary>
        public string BillRefNumber { get; set; }

        /// <summary>
        /// The Short Code receiving the amount being transacted.
        /// </summary>
        public string ShortCode { get; set; }

        /// <summary>
        /// The unique identifier of the transaction type. Possible values are:
        /// - CustomerPayBillOnline: Used for Pay Bills shortcodes.
        /// - CustomerBuyGoodsOnline: Used for Buy Goods shortcodes.
        /// </summary>
        public C2BCommand Command { get; set; } = C2BCommand.CustomerPayBillOnline;
    }
}
