using MpesaLib.Interfaces;
using MpesaLib.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MpesaLib.Clients
{
    /// <summary>
    /// Mpesa Client
    /// </summary>
    public class MpesaClient : IMpesaClient
    {
        private HttpClient _httpclient;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpClient"></param>
        public MpesaClient(HttpClient httpClient)
        {
            _httpclient = httpClient;
        }

        /// <summary>
        /// Base Address
        /// </summary>
        public string BaseUri { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// This methods returns an accesstoken to be passed into the other methods
        /// </summary>
        /// <param name="consumerKey"></param>
        /// <param name="consumerSecret"></param>
        /// <returns></returns>
        public Task<string> GetAuthToken(string consumerKey, string consumerSecret)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Makes a Business to Business payment
        /// </summary>
        /// <param name="b2bitem"></param>
        /// <param name="accesstoken"></param>
        /// <returns></returns>
        public Task<string> MakeB2BPayment(BusinessToBusiness b2bitem, string accesstoken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Makes a Business to Customer payment
        /// </summary>
        /// <param name="b2citem"></param>
        /// <param name="accesstoken"></param>
        /// <returns></returns>
        public Task<string> MakeB2CPayment(BusinessToCustomer b2citem, string accesstoken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Makes a Customer to business payment - Customer initiates transaction
        /// </summary>
        /// <param name="c2bsimulate"></param>
        /// <param name="accesstoken"></param>
        /// <returns></returns>
        public Task<string> MakeC2BPayment(CustomerToBusinessSimulate c2bsimulate, string accesstoken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Makes a customer to business payment - system or business initates transaction through stk push, customer completes transaction
        /// </summary>
        /// <param name="mpesaItem"></param>
        /// <param name="accesstoken"></param>
        /// <returns></returns>
        public Task<string> MakeLipaNaMpesaOnlinePayment(LipaNaMpesaOnline mpesaItem, string accesstoken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Queries business Mpesa account balance
        /// </summary>
        /// <param name="accbalance"></param>
        /// <param name="accesstoken"></param>
        /// <returns></returns>
        public Task<string> QueryAccountBalance(AccountBalance accbalance, string accesstoken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Queries status of a LipaNaMpesa online transaction
        /// </summary>
        /// <param name="mpesaQuery"></param>
        /// <param name="accesstoken"></param>
        /// <returns></returns>
        public Task<string> QueryLipaNaMpesaOnlineTransaction(LipaNaMpesaQuery mpesaQuery, string accesstoken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Queries status of an Mpesa transaction
        /// </summary>
        /// <param name="transactionStatus"></param>
        /// <param name="accesstoken"></param>
        /// <returns></returns>
        public Task<string> QueryMpesaTransactionStatus(MpesaTransactionStatus transactionStatus, string accesstoken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c2bregisterItem"></param>
        /// <param name="accesstoken"></param>
        /// <returns></returns>
        public Task<string> RegisterC2BUrl(CustomerToBusinessRegister c2bregisterItem, string accesstoken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reversal"></param>
        /// <param name="accesstoken"></param>
        /// <returns></returns>
        public Task<string> ReverseMpesaTransaction(Reversal reversal, string accesstoken)
        {
            throw new NotImplementedException();
        }
    }
}
