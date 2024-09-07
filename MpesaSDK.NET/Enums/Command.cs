namespace MpesaSDK.NET.Enums
{
    /// <summary>
    /// This is a unique command that specifies transaction type.
    /// </summary>
    public enum Command
    {
        /// <summary>
        /// This supports sending money to both registere and unregistered M-Pesa customers.
        /// </summary>
        SalaryPayment,
        /// <summary>
        /// This is a normal business to customer payment,  supports only M-Pesa registered customers.
        /// </summary>
        BusinessPayment,
        /// <summary>
        /// This is a promotional payment to customers. The M-Pesa notification message is a congratulatory message. Supports only M-Pesa registered customers.
        /// </summary>
        PromotionPayment,
        /// <summary>
        /// Reversal for an erroneous C2B transaction
        /// </summary>
        TransactionReversal,
        /// <summary>
        /// Used to check the balance in a paybill/buy goods account (includes utility, MMF, Merchant, Charges paid account).
        /// </summary>
        AccountBalance,
        /// <summary>
        /// Used to simulate a transaction taking place in the case of C2B Simulate Transaction or to initiate a transaction on behalf of the customer (STK Push).
        /// </summary>
        CustomerPayBillOnline,
        /// <summary>
        /// Used to simulate a transaction taking place in the case of C2B Simulate Transaction or to initiate a transaction on behalf of the customer (STK Push).
        /// </summary>
        CustomerBuyGoodsOnline,
        /// <summary>
        /// Used to query the details of a transaction.
        /// </summary>
        TransactionStatusQuery,
        /// <summary>
        /// Similar to STK push, uses M-Pesa PIN as a service.
        /// </summary>
        CheckIdentity,
        /// <summary>
        /// Sending funds from one paybill to another paybill
        /// </summary>
        BusinessPayBill,
        /// <summary>
        /// sending funds from buy goods to another buy goods.
        /// </summary>
        BusinessBuyGoods,
        /// <summary>
        /// Transfer of funds from utility to MMF account.
        /// </summary>
        DisburseFundsToBusiness,
        /// <summary>
        /// Transferring funds from one paybills MMF to another paybills MMF account.
        /// </summary>
        BusinessToBusinessTransfer,
        /// <summary>
        /// Transferring funds from paybills MMF to another paybills utility account.
        /// </summary>
        BusinessTransferFromMMFToUtility

    }
}
