using MpesaSDK.NET.Enums;

namespace MpesaSDK.NET.Dtos.InitiateRequests
{
    public class InitiateAccountBalCheck
    {
        /// <summary>
        /// The organization receiving the transaction. This is the party involved in the balance inquiry.
        /// </summary>
        public string PartyA { get; set; }

        /// <summary>
        /// Type of organization receiving the transaction, represented as an identifier.
        /// </summary>
        public IdentifierType IdentifierType { get; set; }

        /// <summary>
        /// The name of the initiator who is making the request.
        /// </summary>
        public string Initiator { get; set; }

        /// <summary>
        /// Password for the initiator.
        /// </summary>
        public string InitiatorPassword { get; set; }

        /// <summary>
        /// The path where information of time-out transactions will be sent. It should contain a valid URI.
        /// </summary>
        public string QueueTimeOutURL { get; set; }

        /// <summary>
        /// The path where the result of the transaction will be sent. It should contain a valid URI.
        /// </summary>
        public string ResultURL { get; set; }

        /// <summary>
        /// Additional comments that are sent along with the transaction, such as "Account Balance Check".
        /// </summary>
        public string Remarks { get; set; }
    }
}
