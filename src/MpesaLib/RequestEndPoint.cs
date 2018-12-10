using System;

namespace MpesaLib
{
    public static class RequestEndPoint 
    {
        public static Uri SandboxBaseAdress { get; set; } = new Uri("https://sandbox.safaricom.co.ke");
        public static Uri LiveBaseAdress { get; set; } = new Uri("https://api.safaricom.co.ke");
        public static string LipaNaMpesaOnline{ get; set; } = "mpesa/stkpush/v1/processrequest";
        public static string QueryLipaNaMpesaOnlieTransaction { get; set; } = "mpesa/stkpushquery/v1/query";
        public static string CustomerToBusinessSimulate{ get; set; } = "mpesa/c2b/v1/simulate";
        public static string BusinessToBusiness{ get; set; } = "mpesa/b2b/v1/paymentrequest";
        public static string BusinessToCustomer{ get; set; } = "mpesa/b2c/v1/paymentrequest";
        public static string AuthToken{ get; set; } = "oauth/v1/generate?grant_type=client_credentials";        
        public static string QueryAccountBalance{ get; set; } = "mpesa/accountbalance/v1/query";
        public static string RegisterC2BUrl{ get; set; } = "mpesa/c2b/v1/registerurl";
        public static string ReverseMpesaTransaction { get; set; } = "mpesa/reversal/v1/request";
        public static string QueryMpesaTransactionStatus{ get; set; } = "mpesa/transactionstatus/v1/query";
    }

    
}
