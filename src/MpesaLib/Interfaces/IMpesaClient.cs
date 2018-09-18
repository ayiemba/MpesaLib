using MpesaLib.Models;
using System.Threading.Tasks;

namespace MpesaLib.Interfaces
{
    /// <summary>
    /// Mpesa API Client interface
    /// </summary>
    public interface IMpesaClient
    {

        /// <summary>
        /// Base Uri
        /// </summary>
        string BaseUri { get; set; }

        /// <summary>
        /// Auth Client
        /// </summary>
        /// <param name="consumerKey"></param>
        /// <param name="consumerSecret"></param>
        /// <returns></returns>
        Task<string> GetAuthToken(string consumerKey, string consumerSecret);

        /// <summary>
        /// LipaNaMpesaOnline Client
        /// </summary>
        /// <param name="mpesaItem"></param>
        /// <param name="accesstoken"></param>
        /// <returns></returns>
        Task<string> MakeLipaNaMpesaOnlinePayment(LipaNaMpesaOnline mpesaItem, string accesstoken);              

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mpesaQuery"></param>
        /// <param name="accesstoken"></param>
        /// <returns></returns>
        Task<string> QueryLipaNaMpesaOnlineTransaction(LipaNaMpesaQuery mpesaQuery, string accesstoken);


        /// <summary>
        /// Account Balance Query Client
        /// </summary>
        /// <param name="accbalance"></param>
        /// <param name="accesstoken"></param>
        /// <returns></returns>
        Task<string> QueryAccountBalance(AccountBalance accbalance, string accesstoken);

        /// <summary>
        /// B2B Client
        /// </summary>
        /// <param name="b2bitem"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<string> MakeB2BPayment(BusinessToBusiness b2bitem, string token);


        /// <summary>
        /// B2C Client
        /// </summary>
        /// <param name="b2citem"></param>
        /// <param name="accesstoken"></param>
        /// <returns></returns>
        Task<string> MakeB2CPayment(BusinessToCustomer b2citem, string accesstoken);


        /// <summary>
        /// C2B Client
        /// </summary>
        /// <param name="c2bsimulate"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<string> MakeC2BPayment(CustomerToBusinessSimulate c2bsimulate, string token);


        /// <summary>
        /// C2B Register Url client
        /// </summary>
        /// <param name="c2bregisterItem"></param>
        /// <param name="accesstoken"></param>
        /// <returns></returns>
        Task<string> RegisterC2BUrl(CustomerToBusinessRegister c2bregisterItem, string accesstoken);

        /// <summary>
        /// Reverse transaction client
        /// </summary>
        /// <param name="reversal"></param>
        /// <param name="accesstoken"></param>
        /// <returns></returns>
        Task<string> ReverseMpesaTransaction(Reversal reversal, string accesstoken);

        /// <summary>
        /// Query transaction status
        /// </summary>
        /// <param name="transactionStatus"></param>
        /// <param name="accesstoken"></param>
        /// <returns></returns>
        Task<string> QueryMpesaTransactionStatus(MpesaTransactionStatus transactionStatus, string accesstoken);
    }
}
