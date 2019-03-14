using System.Threading.Tasks;

namespace MpesaLib
{
    /// <summary>
    /// IMpesaClient Interface
    /// </summary>
    /// <remarks>
    /// Provides all the Methods implemented by MpesaClient Class. 
    /// A developer can create their own implementation for example for tests/mocks by inheriting from this interface.
    /// </remarks>
    public interface IMpesaClient
    {
        /// <summary>
        /// GetAuthTokenAsync is an asynchronous method that requests for and returns an accesstoken from MPESA API Server.
        /// </summary>
        /// <param name="consumerKey">ConsumerKey provided by Safaricom in Daraja Portal.</param>
        /// <param name="consumerSecret">ConsumerSecret provided by Safaricom in Daraja Portal.</param>
        /// <param name="requestEndPoint">Set to <c>RequestEndPoint.AuthToken</c></param>
        /// <returns>A string of characters representing the accesstoken.</returns>
        Task<string> GetAuthTokenAsync(string consumerKey, string consumerSecret, string requestEndPoint);

        /// <summary>
        /// GetAuthTokenAsync is an asynchronous method that requests for and returns an accesstoken from MPESA API Server.
        /// </summary>
        /// <param name="consumerKey">ConsumerKey provided by Safaricom in Daraja Portal.</param>
        /// <param name="consumerSecret">ConsumerSecret provided by Safaricom in Daraja Portal.</param>
        /// <param name="requestEndPoint">Set to <c>RequestEndPoint.AuthToken</c></param>
        /// <returns>A string of characters representing the accesstoken.</returns>
        string GetAuthToken(string consumerKey, string consumerSecret, string requestEndPoint);


        /// <summary>
        /// Makes an STK Push payment request to MPESA API Server.
        /// </summary>
        /// <param name="mpesaLipaOnlineDto">
        /// Data trnasfer object containing properties for the Lipa Na Mpesa Online API endpoint.
        /// </param>
        /// <param name="accesstoken">Acccesstoken retrieved by the <c>GetAuthTokenAsync</c> method.</param>
        /// <param name="requestEndPoint">Set to <c>RequestEndPoint.LipaNaMpesaOnline</c></param>
        /// <returns>A JSON string containing LNMO response data from MPESA API Server</returns>
        Task<string> MakeLipaNaMpesaOnlinePaymentAsync(LipaNaMpesaOnlineDto mpesaLipaOnlineDto, string accesstoken, string requestEndPoint);


        /// <summary>
        /// Makes an STK Push payment request to MPESA API Server.
        /// </summary>
        /// <param name="mpesaLipaOnlineDto">
        /// Data trnasfer object containing properties for the Lipa Na Mpesa Online API endpoint.
        /// </param>
        /// <param name="accesstoken">Acccesstoken retrieved by the <c>GetAuthTokenAsync</c> method.</param>
        /// <param name="requestEndPoint">Set to <c>RequestEndPoint.LipaNaMpesaOnline</c></param>
        /// <returns>A JSON string containing data from MPESA API response</returns>
        string MakeLipaNaMpesaOnlinePayment(LipaNaMpesaOnlineDto mpesaLipaOnlineDto, string accesstoken, string requestEndPoint);


        /// <summary>
        /// Queries Mpesa Online Transaction Status
        /// </summary>
        /// <param name="mpesaTransactionQueryDto">Transaction Query Data transfer object</param>
        /// <param name="accesstoken">Acccesstoken retrieved by the <c>GetAuthTokenAsync</c> method.</param>
        /// <param name="requestEndPoint">Set to <c>RequestEndPoint.QueryLipaNaMpesaOnlieTransaction</c></param>
        /// <returns>
        /// A JSON string containing data from MPESA API reposnse
        /// </returns>
        /// <remarks>
        /// Use only for transactions initiated with <c>MakeLipaNaMpesaOnlinePayment</c> method.
        /// For Other transaction based methods (C2B,B2C,B2B) use <c>QueryMpesaTransactionStatusAsync</c> method.
        /// </remarks>
        Task<string> QueryLipaNaMpesaTransactionAsync(LipaNaMpesaQueryDto mpesaTransactionQueryDto, string accesstoken, string requestEndPoint);


        /// <summary>
        /// Queries Mpesa Online Transaction Status.
        /// </summary>
        /// <param name="mpesaTransactionQueryDto">Transaction Query Data transfer object.</param>
        /// <param name="accesstoken">Acccesstoken retrieved by the <c>GetAuthTokenAsync</c> method.</param>
        /// <param name="requestEndPoint">Set to <c>RequestEndPoint.QueryLipaNaMpesaOnlieTransaction</c></param>
        /// <returns>
        /// A JSON string containing data from MPESA API reposnse.
        /// </returns>
        /// <remarks>
        /// Use only for transactions initiated with <c>MakeLipaNaMpesaOnlinePayment</c> method.
        /// For Other transaction based methods (C2B, B2C, B2B, Accountbalance, Reversal) 
        /// use <c>QueryMpesaTransactionStatusAsync</c> method.
        /// </remarks>
        string QueryLipaNaMpesaTransaction(LipaNaMpesaQueryDto mpesaTransactionQueryDto, string accesstoken, string requestEndPoint);


        /// <summary>
        /// Queries MPESA Paybill Account balance.
        /// </summary>
        /// <param name="accountBalanceQueryDto">Account balance query data transfer object.</param>
        /// <param name="accesstoken">Acccesstoken retrieved by the <c>GetAuthTokenAsync</c> method.</param>
        /// <param name="requestEndPoint">Set to <c>RequestEndPoint.QueryAccountBalance</c></param>
        /// <returns>A JSON string containing data from MPESA API reposnse.</returns>
        Task<string> QueryAccountBalanceAsync(AccountBalanceDto accountBalanceQueryDto, string accesstoken, string requestEndPoint);


        /// <summary>
        /// Queries MPESA Paybill Account balance.
        /// </summary>
        /// <param name="accountBalanceQueryDto">Account balance query data transfer object.</param>
        /// <param name="accesstoken">Acccesstoken retrieved by the <c>GetAuthTokenAsync</c> method.</param>
        /// <param name="requestEndPoint">Set to <c>RequestEndPoint.QueryAccountBalance</c></param>
        /// <returns>A JSON string containing data from MPESA API reposnse.</returns>
        string QueryAccountBalance(AccountBalanceDto accountBalanceQueryDto, string accesstoken, string requestEndPoint);


        /// <summary>
        /// Makes a Business to Business payment request between Paybill numbers.
        /// </summary>
        /// <param name="businessToBusinessDto">B2B data transfer object.</param>
        /// <param name="accesstoken">Acccesstoken retrieved by the <c>GetAuthTokenAsync</c> method.</param>
        /// <param name="requestEndPoint">Set to <c>RequestEndPoint.BusinessToBusiness</c></param>
        /// <returns>A JSON string containing data from MPESA API reposnse.</returns>
        Task<string> MakeB2BPaymentAsync(BusinessToBusinessDto businessToBusinessDto, string accesstoken, string requestEndPoint);


        /// <summary>
        /// Makes a Business to Business payment request between Paybill numbers.
        /// </summary>
        /// <param name="businessToBusinessDto">B2B data transfer object.</param>
        /// <param name="accesstoken">Acccesstoken retrieved by the <c>GetAuthTokenAsync</c> method.</param>
        /// <param name="requestEndPoint">Set to <c>RequestEndPoint.BusinessToBusiness</c></param>
        /// <returns>A JSON string containing data from MPESA API reposnse.</returns>
        string MakeB2BPayment(BusinessToBusinessDto businessToBusinessDto, string accesstoken, string requestEndPoint);



        /// <summary>
        /// Makes a Business to Customer payment request. Paybill to Phone Number.
        /// </summary>
        /// <param name="businessToCustomerDto">B2C data transfer object.</param>
        /// <param name="accesstoken">Acccesstoken retrieved by the <c>GetAuthTokenAsync</c> method.</param>
        /// <param name="requestEndPoint">Set to <c>RequestEndPoint.BusinessToCustomer</c></param>
        /// <returns>A JSON string containing data from MPESA API reposnse.</returns>
        /// <remarks>
        /// Suitable for refunds, rewards or just about any transaction that involves a business paying a customer.
        /// </remarks>
        Task<string> MakeB2CPaymentAsync(BusinessToCustomerDto businessToCustomerDto, string accesstoken, string requestEndPoint);


        /// <summary>
        /// Makes a Business to Customer payment request. Paybill to Phone Number.
        /// </summary>
        /// <param name="businessToCustomerDto">B2C data transfer object.</param>
        /// <param name="accesstoken">Acccesstoken retrieved by the <c>GetAuthTokenAsync</c> method.</param>
        /// <param name="requestEndPoint">Set to <c>RequestEndPoint.BusinessToCustomer</c></param>
        /// <returns>A JSON string containing data from MPESA API reposnse.</returns>
        /// <remarks>
        /// Suitable for refunds, rewards or just about any transaction that involves a business paying a customer.
        /// </remarks>
        string MakeB2CPayment(BusinessToCustomerDto businessToCustomerDto, string accesstoken, string requestEndPoint);


        /// <summary>
        /// Simulates a Customer to Business payment request.
        /// </summary>
        /// <param name="customerToBusinessSimulateDto">C2B data transfer object</param>
        /// <param name="accesstoken">Acccesstoken retrieved by the <c>GetAuthTokenAsync</c> method.</param>
        /// <param name="requestEndPoint">Set to <c>RequestEndPoint.CustomerToBusinessSimulate</c></param>
        /// <returns>A JSON string containing data from MPESA API reposnse.</returns>
        /// <remarks>
        /// Use only for Simulation/testing. In production use <c>RegisterC2BUrlAsync</c> method to register 
        /// endpoints in your application that receive customer initiated transactions from the MPESA API
        /// for confirmation and/or validation
        /// </remarks>
        Task<string> MakeC2BPaymentAsync(CustomerToBusinessSimulateDto customerToBusinessSimulateDto, string accesstoken, string requestEndPoint);

        /// <summary>
        /// Simulates a Customer to Business payment request.
        /// </summary>
        /// <param name="customerToBusinessSimulateDto">C2B data transfer object</param>
        /// <param name="accesstoken">Acccesstoken retrieved by the <c>GetAuthTokenAsync</c> method.</param>
        /// <param name="requestEndPoint">Set to <c>RequestEndPoint.CustomerToBusinessSimulate</c></param>
        /// <returns>A JSON string containing data from MPESA API reposnse.</returns>
        /// <remarks>
        /// Use only for Simulation/testing. In production use <c>RegisterC2BUrlAsync</c> method to register 
        /// endpoints in your application that receive customer initiated transactions from the MPESA API
        /// for confirmation and/or validation
        /// </remarks>
        string MakeC2BPayment(CustomerToBusinessSimulateDto customerToBusinessSimulateDto, string accesstoken, string requestEndPoint);


        /// <summary>
        /// Registers Customer to Business Confirmation and Validation URLs.
        /// </summary>
        /// <param name="customerToBusinessRegisterUrlDto">C2B Register URLs data transfer object.</param>
        /// <param name="accesstoken">Acccesstoken retrieved by the <c>GetAuthTokenAsync</c> method.</param>
        /// <param name="requestEndPoint">Set to <c>RequestEndPoint.RegisterC2BUrl</c></param>
        /// <returns>A JSON string containing data from MPESA API reposnse.</returns>
        Task<string> RegisterC2BUrlAsync(CustomerToBusinessRegisterUrlDto customerToBusinessRegisterUrlDto, string accesstoken, string requestEndPoint);


        /// <summary>
        /// Registers Customer to Business Confirmation and Validation URLs.
        /// </summary>
        /// <param name="customerToBusinessRegisterUrlDto">C2B Register URLs data transfer object.</param>
        /// <param name="accesstoken">Acccesstoken retrieved by the <c>GetAuthTokenAsync</c> method.</param>
        /// <param name="requestEndPoint">Set to <c>RequestEndPoint.RegisterC2BUrl</c></param>
        /// <returns>A JSON string containing data from MPESA API reposnse.</returns>
        string RegisterC2BUrl(CustomerToBusinessRegisterUrlDto customerToBusinessRegisterUrlDto, string accesstoken, string requestEndPoint);


        /// <summary>
        /// Reverses a Mpesa Transaction.
        /// </summary>
        /// <param name="reversalDto">Reversal data transfer object.</param>
        /// <param name="accesstoken">Acccesstoken retrieved by the <c>GetAuthTokenAsync</c> method.</param>
        /// <param name="requestEndPoint">Set to <c>RequestEndPoint.ReverseMpesaTransaction</c></param>
        /// <returns>A JSON string containing data from MPESA API reposnse.</returns>
        Task<string> ReverseMpesaTransactionAsync(ReversalDto reversalDto, string accesstoken, string requestEndPoint);


        /// <summary>
        /// Reverses a Mpesa Transaction.
        /// </summary>
        /// <param name="reversalDto">Reversal data transfer object.</param>
        /// <param name="accesstoken">Acccesstoken retrieved by the <c>GetAuthTokenAsync</c> method.</param>
        /// <param name="requestEndPoint">Set to <c>RequestEndPoint.ReverseMpesaTransaction</c></param>
        /// <returns>A JSON string containing data from MPESA API reposnse.</returns>
        string ReverseMpesaTransaction(ReversalDto reversalDto, string accesstoken, string requestEndPoint);


        /// <summary>
        /// Queries the status of an Mpesa Transaction.
        /// </summary>
        /// <param name="mpesaTransactionStatusDto">Transaction Status data transfer object.</param>
        /// <param name="accesstoken">Acccesstoken retrieved by the <c>GetAuthTokenAsync</c> method.</param>
        /// <param name="requestEndPoint">Set to <c>RequestEndPoint.QueryMpesaTransactionStatus</c></param>
        /// <returns>A JSON string containing data from MPESA API reposnse.</returns>
        Task<string> QueryMpesaTransactionStatusAsync(MpesaTransactionStatusDto mpesaTransactionStatusDto, string accesstoken, string requestEndPoint);


        /// <summary>
        /// Queries the status of an Mpesa Transaction.
        /// </summary>
        /// <param name="mpesaTransactionStatusDto">Transaction Status data transfer object.</param>
        /// <param name="accesstoken">Acccesstoken retrieved by the <c>GetAuthTokenAsync</c> method.</param>
        /// <param name="requestEndPoint">Set to <c>RequestEndPoint.QueryMpesaTransactionStatus</c></param>
        /// <returns>A JSON string containing data from MPESA API reposnse.</returns>
        string QueryMpesaTransactionStatus(MpesaTransactionStatusDto mpesaTransactionStatusDto, string accesstoken, string requestEndPoint);
    }
}
