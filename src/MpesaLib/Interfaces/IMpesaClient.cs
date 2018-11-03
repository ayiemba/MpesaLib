using MpesaLib.Models;
using System;
using System.Threading.Tasks;

namespace MpesaLib.Interfaces
{
    /// <summary>
    /// Mpesa API Client interface
    /// </summary>
    public interface IMpesaClient
    {       

        /// <summary>
        /// Auth Client
        /// </summary>
        /// <param name="consumerKey"></param>
        /// <param name="consumerSecret"></param>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        Task<string> GetAuthTokenAsync(string consumerKey, string consumerSecret, string requestUri);
        //string GetAuthToken(string consumerKey, string consumerSecret, string requestUri);

        /// <summary>
        /// LipaNaMpesaOnline Client
        /// </summary>
        /// <param name="mpesaItem"></param>
        /// <param name="accesstoken"></param>
        /// <param name="requestUri"></param>       
        /// <returns></returns>
        Task<string> MakeLipaNaMpesaOnlinePaymentAsync(LipaNaMpesaOnline mpesaItem, string accesstoken, string requestUri);
        //string MakeLipaNaMpesaOnlinePayment(LipaNaMpesaOnline mpesaItem, string accesstoken, string requestUri);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mpesaQuery"></param>
        /// <param name="accesstoken"></param>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        Task<string> QueryLipaNaMpesaTransactionAsync(LipaNaMpesaQuery mpesaQuery, string accesstoken, string requestUri);
        //string QueryLipaNaMpesaTransaction(LipaNaMpesaQuery mpesaQuery, string accesstoken, string requestUri);


        /// <summary>
        /// Account Balance Query Client
        /// </summary>
        /// <param name="accbalance"></param>
        /// <param name="accesstoken"></param>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        Task<string> QueryAccountBalanceAsync(AccountBalance accbalance, string accesstoken, string requestUri);
        //string QueryAccountBalance(AccountBalance accbalance, string accesstoken, string requestUri);

        /// <summary>
        /// B2B Client
        /// </summary>
        /// <param name="b2bitem"></param>
        /// <param name="token"></param>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        Task<string> MakeB2BPaymentAsync(BusinessToBusiness b2bitem, string token, string requestUri);
        //string MakeB2BPayment(BusinessToBusiness b2bitem, string token, string requestUri);


        /// <summary>
        /// B2C Client
        /// </summary>
        /// <param name="b2citem"></param>
        /// <param name="accesstoken"></param>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        Task<string> MakeB2CPaymentAsync(BusinessToCustomer b2citem, string accesstoken, string requestUri);
        //string MakeB2CPayment(BusinessToCustomer b2citem, string accesstoken, string requestUri);


        /// <summary>
        /// C2B Client
        /// </summary>
        /// <param name="c2bsimulate"></param>
        /// <param name="token"></param>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        Task<string> MakeC2BPaymentAsync(CustomerToBusinessSimulate c2bsimulate, string token, string requestUri);
        //string MakeC2BPayment(CustomerToBusinessSimulate c2bsimulate, string token, string requestUri);


        /// <summary>
        /// C2B Register Url client
        /// </summary>
        /// <param name="c2bregisterItem"></param>
        /// <param name="accesstoken"></param>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        Task<string> RegisterC2BUrlAsync(CustomerToBusinessRegister c2bregisterItem, string accesstoken, string requestUri);
        //string RegisterC2BUrl(CustomerToBusinessRegister c2bregisterItem, string accesstoken, string requestUri);

        /// <summary>
        /// Reverse transaction client
        /// </summary>
        /// <param name="reversal"></param>
        /// <param name="accesstoken"></param>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        Task<string> ReverseMpesaTransactionAsync(Reversal reversal, string accesstoken, string requestUri);
        //string ReverseMpesaTransaction(Reversal reversal, string accesstoken, string requestUri);

        /// <summary>
        /// Query transaction status
        /// </summary>
        /// <param name="transactionStatus"></param>
        /// <param name="accesstoken"></param>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        Task<string> QueryMpesaTransactionStatusAsync(MpesaTransactionStatus transactionStatus, string accesstoken, string requestUri);
        //string QueryMpesaTransactionStatus(MpesaTransactionStatus transactionStatus, string accesstoken, string requestUri);
    }
}
