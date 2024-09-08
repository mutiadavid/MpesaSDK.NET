namespace MpesaSDK.NET.Enums
{
    public enum QueryCommand
    {
        /// <summary>
        /// Command for enquiring account balance.
        /// </summary>
        AccountBalance,

        /// <summary>
        /// Command for enquiring the transaction status.
        /// </summary>
        TransactionStatusQuery,

        /// <summary>
        /// Command for reversing the transaction.
        /// </summary>
        TransactionReversal
    }
}
