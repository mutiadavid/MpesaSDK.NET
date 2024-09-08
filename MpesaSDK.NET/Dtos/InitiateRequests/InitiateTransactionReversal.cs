using MpesaSDK.NET.Enums;

namespace MpesaSDK.NET.Dtos.InitiateRequests
{
    public class InitiateTransactionReversal
    {
        /// <summary>
        /// The name of the initiator making the reversal request, typically the organization or MSISDN.
        /// </summary>
        public string Initiator { get; set; }

        /// <summary>
        /// Password for the initiator.
        /// </summary>
        public string InitiatorPassword { get; set; }

        /// <summary>
        /// The shortcode (6 digits) of the organization receiving the reversed transaction.
        /// </summary>
        public string ReceiverParty { get; set; }

        /// <summary>
        /// Type of the organization receiving the reversed transaction, represented as an identifier.
        /// </summary>
        public IdentifierType RecieverIdentifierType { get; set; }

        /// <summary>
        /// The transaction ID of the transaction that is being reversed (e.g., LKXXXX1234).
        /// </summary>
        public string TransactionID { get; set; }

        /// <summary>
        /// The amount to reverse from the transaction.
        /// </summary>
        public long Amount { get; set; }

        /// <summary>
        /// The URL to notify in case of a time-out during the reversal process. It should contain a valid URI.
        /// </summary>
        public string QueueTimeOutURL { get; set; }

        /// <summary>
        /// The URL to receive the result of the transaction reversal. It should contain a valid URI.
        /// </summary>
        public string ResultURL { get; set; }

        /// <summary>
        /// Optional parameter that references a specific occasion, represented as a string with a maximum of 100 characters.
        /// </summary>
        public string Occasion { get; set; }

        /// <summary>
        /// Additional comments or remarks accompanying the reversal transaction, with a maximum length of 100 characters.
        /// </summary>
        public string Remarks { get; set; }
    }
}
