namespace MpesaSDK.NET.Enums
{
    public enum B2CCommand
    {
        /// <summary>
        /// Supports sending money to both registered and unregistered M-Pesa customers.
        /// </summary>
        SalaryPayment,

        /// <summary>
        /// A normal business to customer payment, supports only M-Pesa registered customers.
        /// </summary>
        BusinessPayment,

        /// <summary>
        /// A promotional payment to customers with a congratulatory message. Supports only M-Pesa registered customers.
        /// </summary>
        PromotionPayment
    }
}
