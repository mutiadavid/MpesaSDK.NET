using MpesaSDK.NET.Enums;

namespace MpesaSDK.NET.Dtos.InitiateRequests
{
    public class InitiateB2C
    {
        /// <summary>
        /// The username of the M-Pesa B2C account API operator. 
        /// NOTE: The access channel for this operator must be API, and the account must be in active status.
        /// </summary>
        public string InitiatorName { get; set; }

        /// <summary>
        /// Password for the initiator.
        /// </summary>
        public string InitiatorPassword { get; set; }

        /// <summary>
        /// A unique command that specifies the B2C transaction type.
        /// - SalaryPayment: Supports sending money to both registered and unregistered M-Pesa customers.
        /// - BusinessPayment: A normal business to customer payment, supports only M-Pesa registered customers.
        /// - PromotionPayment: A promotional payment to customers with a congratulatory message. Supports only M-Pesa registered customers.
        /// </summary>
        public B2CCommand Command { get; set; }

        /// <summary>
        /// The amount of money being sent to the customer.
        /// </summary>
        public long Amount { get; set; }

        /// <summary>
        /// The B2C organization shortcode from which the money is to be sent.
        /// </summary>
        public string PartyA { get; set; }

        /// <summary>
        /// The customer mobile number to receive the amount. 
        /// The number should have the country code (254) without the plus sign.
        /// </summary>
        public string PartyB { get; set; }

        /// <summary>
        /// Any additional information to be associated with the transaction.
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// URL to be used by the API Proxy to send a notification if the payment request times out while awaiting processing in the queue.
        /// </summary>
        public string QueueTimeOutURL { get; set; }

        /// <summary>
        /// URL to be used by M-Pesa to send a notification upon processing of the payment request.
        /// </summary>
        public string ResultURL { get; set; }

        /// <summary>
        /// Any additional information to be associated with the transaction.
        /// </summary>
        public string Occassion { get; set; }
    }
}
