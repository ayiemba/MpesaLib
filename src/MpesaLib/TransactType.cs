namespace MpesaLib
{
    /// <summary>
    /// Trasaction Types and/or Command IDs
    /// </summary>
    public static class TransactType
    {
        /// <summary>
        /// CustomerPayBillOnline Command ID
        /// </summary>
        public const string CustomerPayBillOnline = "CustomerPayBillOnline";

        /// <summary>
        /// CustomerBuyGoodsOnline Command ID
        /// </summary>
        public const string CustomerBuyGoodsOnline = "CustomerBuyGoodsOnline";

        /// <summary>
        /// TransactionStatusQuery Command ID
        /// </summary>
        public const string TransactionStatusQuery = "TransactionStatusQuery";

        /// <summary>
        /// TransactionReversal Command ID
        /// </summary>
        public const string TransactionReversal = "TransactionReversal";

        /// <summary>
        /// BusinessPayBill Command ID
        /// </summary>
        public const string BusinessPayBill = "BusinessPayBill";

        /// <summary>
        /// MerchantToMerchantTransfer Command ID
        /// </summary>
        public const string MerchantToMerchantTransfer = "MerchantToMerchantTransfer";

        /// <summary>
        /// MerchantTransferFromMerchantToWorking Command ID
        /// </summary>
        public const string MerchantTransferFromMerchantToWorking = "MerchantTransferFromMerchantToWorking";

        /// <summary>
        /// MerchantServicesMMFAccountTransfer Command ID
        /// </summary>
        public const string MerchantServicesMMFAccountTransfer = "MerchantServicesMMFAccountTransfer";

        /// <summary>
        /// AgencyFloatAdvance Command ID
        /// </summary>
        public const string AgencyFloatAdvance = "AgencyFloatAdvance";

        /// <summary>
        /// Use with B2C. Supports sending money to both registered and unregistered M-Pesa customers
        /// </summary>
        public const string SalaryPayment = "SalaryPayment";

        /// <summary>
        /// USe with B2C. A normal business to customer payment, supports only M-Pesa registered customers
        /// </summary>
        public const string BusinessPayment = "BusinessPayment";

        /// <summary>
        /// Use with B2C. A promotional payment to customers.The M-Pesa notification message is a 
        /// congratulatory message. Supports only M-Pesa registered customers.
        /// </summary>
        public const string PromotionPayment = "PromotionPayment";

    }

}
