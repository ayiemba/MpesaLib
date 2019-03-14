using MpesaLib.Models;
using System;
using System.Threading.Tasks;

namespace MpesaLib.Interfaces
{
    public interface IMpesaClient
    {        
        Task<string> GetAuthTokenAsync(string consumerKey, string consumerSecret, string requestUri);
        //string GetAuthToken(string consumerKey, string consumerSecret, string requestUri);
      
        Task<string> MakeLipaNaMpesaOnlinePaymentAsync(LipaNaMpesaOnline mpesaItem, string accesstoken, string requestUri);
        //string MakeLipaNaMpesaOnlinePayment(LipaNaMpesaOnline mpesaItem, string accesstoken, string requestUri);
       
        Task<string> QueryLipaNaMpesaTransactionAsync(LipaNaMpesaQuery mpesaQuery, string accesstoken, string requestUri);
        //string QueryLipaNaMpesaTransaction(LipaNaMpesaQuery mpesaQuery, string accesstoken, string requestUri);
       
        Task<string> QueryAccountBalanceAsync(AccountBalance accbalance, string accesstoken, string requestUri);
        //string QueryAccountBalance(AccountBalance accbalance, string accesstoken, string requestUri);
        
        Task<string> MakeB2BPaymentAsync(BusinessToBusiness b2bitem, string token, string requestUri);
        //string MakeB2BPayment(BusinessToBusiness b2bitem, string token, string requestUri);
      
        Task<string> MakeB2CPaymentAsync(BusinessToCustomer b2citem, string accesstoken, string requestUri);
        //string MakeB2CPayment(BusinessToCustomer b2citem, string accesstoken, string requestUri);
        
        Task<string> MakeC2BPaymentAsync(CustomerToBusinessSimulate c2bsimulate, string token, string requestUri);
        //string MakeC2BPayment(CustomerToBusinessSimulate c2bsimulate, string token, string requestUri);
     
        Task<string> RegisterC2BUrlAsync(CustomerToBusinessRegister c2bregisterItem, string accesstoken, string requestUri);
        //string RegisterC2BUrl(CustomerToBusinessRegister c2bregisterItem, string accesstoken, string requestUri);
      
        Task<string> ReverseMpesaTransactionAsync(Reversal reversal, string accesstoken, string requestUri);
        //string ReverseMpesaTransaction(Reversal reversal, string accesstoken, string requestUri);
     
        Task<string> QueryMpesaTransactionStatusAsync(MpesaTransactionStatus transactionStatus, string accesstoken, string requestUri);
        //string QueryMpesaTransactionStatus(MpesaTransactionStatus transactionStatus, string accesstoken, string requestUri);
    }
}
