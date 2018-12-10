using System.Threading.Tasks;

namespace MpesaLib
{
    
    public interface IMpesaClient
    {       
        
        Task<string> GetAuthTokenAsync(string consumerKey, string consumerSecret, string requestEndPoint);
        //string GetAuthToken(string consumerKey, string consumerSecret, string requestUri);

       
        Task<string> MakeLipaNaMpesaOnlinePaymentAsync(LipaNaMpesaOnlineDto mpesaLipaOnlineDto, string accesstoken, string requestEndPoint);
        //string MakeLipaNaMpesaOnlinePayment(LipaNaMpesaOnline mpesaItem, string accesstoken, string requestUri);

       
        Task<string> QueryLipaNaMpesaTransactionAsync(LipaNaMpesaQueryDto mpesaTransactionQueryDto, string accesstoken, string requestEndPoint);
        //string QueryLipaNaMpesaTransaction(LipaNaMpesaQuery mpesaQuery, string accesstoken, string requestUri);

       
        Task<string> QueryAccountBalanceAsync(AccountBalanceDto accountBalanceQueryDto, string accesstoken, string requestEndPoint);
        //string QueryAccountBalance(AccountBalance accbalance, string accesstoken, string requestUri);

       
        Task<string> MakeB2BPaymentAsync(BusinessToBusinessDto businessToBusinessDto, string accesstoken, string requestEndPoint);
        //string MakeB2BPayment(BusinessToBusiness b2bitem, string token, string requestUri);


        
        Task<string> MakeB2CPaymentAsync(BusinessToCustomerDto businessToCustomerDto, string accesstoken, string requestEndPoint);
        //string MakeB2CPayment(BusinessToCustomer b2citem, string accesstoken, string requestUri);


        
        Task<string> MakeC2BPaymentAsync(CustomerToBusinessSimulateDto customerToBusinessSimulateDto, string accesstoken, string requestEndPoint);
        //string MakeC2BPayment(CustomerToBusinessSimulate c2bsimulate, string token, string requestUri);


        Task<string> RegisterC2BUrlAsync(CustomerToBusinessRegisterUrlDto customerToBusinessRegisterUrlDto, string accesstoken, string requestEndPoint);
        //string RegisterC2BUrl(CustomerToBusinessRegister c2bregisterItem, string accesstoken, string requestUri);

        
        Task<string> ReverseMpesaTransactionAsync(ReversalDto reversalDto, string accesstoken, string requestEndPoint);
        //string ReverseMpesaTransaction(Reversal reversal, string accesstoken, string requestUri);

        
        Task<string> QueryMpesaTransactionStatusAsync(MpesaTransactionStatusDto mpesaTransactionStatusDto, string accesstoken, string requestEndPoint);
        //string QueryMpesaTransactionStatus(MpesaTransactionStatus transactionStatus, string accesstoken, string requestUri);
    }
}
