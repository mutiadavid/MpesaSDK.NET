using MpesaSDK.NET.Enums;

namespace MpesaSDK.NET.Dtos.InitiateRequests
{
    public class InitiateB2B
    {
        /// <summary>
        /// The credential/username used to authenticate the transaction request.
        /// </summary>
        public string Initiator { get; set; }

        /// <summary>
        /// Password for the initiator.
        /// </summary>
        public string InitiatorPassword { get; set; }

        /// <summary>
        /// Unique command for each transaction type. Possible values are:
        /// - BusinessPayBill
        /// - MerchantToMerchantTransfer
        /// - MerchantTransferFromMerchantToWorking
        /// - MerchantServicesMMFAccountTransfer
        /// - AgencyFloatAdvance
        /// </summary>
        public B2CCommand Command { get; set; }

        /// <summary>
        /// The amount being transacted.
        /// </summary>
        public long Amount { get; set; }

        /// <summary>
        /// Organization’s short code initiating the transaction.
        /// </summary>
        public string PartyA { get; set; }

        /// <summary>
        /// Organization’s short code receiving the funds being transacted.
        /// </summary>
        public string PartyB { get; set; }

        /// <summary>
        /// Type of organization sending the transaction.
        /// </summary>
        public IdentifierType SenderIdentifier { get; set; }

        /// <summary>
        /// Type of organization receiving the funds being transacted.
        /// </summary>
        public IdentifierType ReceiverIdentifierType { get; set; }

        /// <summary>
        /// Comments that are sent along with the transaction.
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// The path that stores information of timeout transactions. It should be validated to ensure it contains the port, URI, domain name, or a publicly available IP.
        /// </summary>
        public string QueueTimeOutURL { get; set; }

        /// <summary>
        /// The path that receives results from M-Pesa. It should be validated to ensure it contains the port, URI, domain name, or a publicly available IP.
        /// </summary>
        public string ResultURL { get; set; }

        /// <summary>
        /// Account Reference, mandatory for the “BusinessPayBill” CommandID.
        /// </summary>
        public string AccountReference { get; set; }
    }
}
