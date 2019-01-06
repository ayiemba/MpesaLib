using System;

namespace MpesaLib
{
    /// <summary>
    /// Provides Strongly typed properties from the MPESA API request endpoints
    /// </summary>
    public static class RequestEndPoint 
    {
        /// <summary>
        /// Sandbox MPESA API BaseAdress, use in a development environment
        /// </summary>
        public static Uri SandboxBaseAddress { get; set; } = new Uri("https://sandbox.safaricom.co.ke/");

        /// <summary>
        /// Live MPESA API BaseAdress, use in staging, production  environments
        /// </summary>
        public static Uri LiveBaseAddress { get; set; } = new Uri("https://api.safaricom.co.ke/");

        /// <summary>
        /// LipaNaMpesaOnline (STK Push) API Request Endpoint
        /// </summary>
        public static string LipaNaMpesaOnline { get; set; } = "mpesa/stkpush/v1/processrequest";

        /// <summary>
        /// QueryLipaNaMpesaOnlieTransaction API Request Endpoint
        /// </summary>
        public static string QueryLipaNaMpesaOnlieTransaction { get; set; } = "mpesa/stkpushquery/v1/query";

        /// <summary>
        /// CustomerToBusinessSimulate API Request Endpoint
        /// </summary>
        public static string CustomerToBusinessSimulate { get; set; } = "mpesa/c2b/v1/simulate";

        /// <summary>
        /// BusinessToBusiness API Request Endpoint
        /// </summary>
        public static string BusinessToBusiness { get; set; } = "mpesa/b2b/v1/paymentrequest";

        /// <summary>
        /// BusinessToCustomer API Request Endpoint
        /// </summary>
        public static string BusinessToCustomer { get; set; } = "mpesa/b2c/v1/paymentrequest";

        /// <summary>
        /// AuthToken Request API Endpoint
        /// </summary>
        public static string AuthToken { get; set; } = "oauth/v1/generate?grant_type=client_credentials";

        /// <summary>
        /// QueryAccountBalance API Request Endpoint
        /// </summary>
        public static string QueryAccountBalance { get; set; } = "mpesa/accountbalance/v1/query";

        /// <summary>
        /// RegisterC2BUrl API Request Endpoint
        /// </summary>
        public static string RegisterC2BUrl { get; set; } = "mpesa/c2b/v1/registerurl";

        /// <summary>
        /// ReverseMpesaTransaction API Request Endpoint
        /// </summary>
        public static string ReverseMpesaTransaction { get; set; } = "mpesa/reversal/v1/request";

        /// <summary>
        /// QueryMpesaTransactionStatus API Request Endpoint
        /// </summary>
        public static string QueryMpesaTransactionStatus { get; set; } = "mpesa/transactionstatus/v1/query";
    }

    
}
