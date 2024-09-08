namespace MpesaSDK.NET.Enums
{
    public enum B2BCommand
    {
        /// <summary>
        /// Used for Business PayBill transactions.
        /// </summary>
        BusinessPayBill,

        /// <summary>
        /// Used for Merchant to Merchant Transfer transactions.
        /// </summary>
        MerchantToMerchantTransfer,

        /// <summary>
        /// Used for Merchant Transfer from Merchant to Working Account transactions.
        /// </summary>
        MerchantTransferFromMerchantToWorking,

        /// <summary>
        /// Used for Merchant Services MMF Account Transfer transactions.
        /// </summary>
        MerchantServicesMMFAccountTransfer,

        /// <summary>
        /// Used for Agency Float Advance transactions.
        /// </summary>
        AgencyFloatAdvance
    }
}
