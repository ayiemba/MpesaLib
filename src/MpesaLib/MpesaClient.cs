using MpesaLib.Helpers.Exceptions;
using MpesaLib.Responses;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MpesaLib
{
    /// <summary>
    /// Mpesa Client Class provides all the methods that make the different API calls to MPESA Server
    /// </summary>
    public class MpesaClient : IMpesaClient
    {
        private readonly HttpClient _httpclient;

        /// <summary>
        /// MpesaClient takes in an instance of HttpClient
        /// </summary>
        /// <param name="httpClient">HttpClient Instance</param>        
        public MpesaClient(HttpClient httpClient)
        {
            _httpclient = httpClient;
        }

        /// <summary>
        /// GetAuthTokenAsync is an asynchronous method that requests for and returns an accesstoken from MPESA API Server.
        /// </summary>
        /// <param name="consumerKey">ConsumerKey provided by Safaricom in Daraja Portal.</param>
        /// <param name="consumerSecret">ConsumerSecret provided by Safaricom in Daraja Portal.</param>
        /// <param name="requestEndPoint">Set to <c>RequestEndPoint.AuthToken</c></param>
        /// <returns>A string of characters representing the accesstoken.</returns>
        public string GetAuthToken(string consumerKey, string consumerSecret, string requestEndPoint)
        {
            return RequestAccessToken(consumerKey, consumerSecret, requestEndPoint).GetAwaiter().GetResult();
        }

        /// <summary>
        /// GetAuthTokenAsync is an asynchronous method that requests for and returns an accesstoken from MPESA API Server.
        /// </summary>
        /// <param name="consumerKey">ConsumerKey provided by Safaricom in Daraja Portal.</param>
        /// <param name="consumerSecret">ConsumerSecret provided by Safaricom in Daraja Portal.</param>
        /// <param name="requestEndPoint">Set to <c>RequestEndPoint.AuthToken</c></param>
        /// <returns>A string of characters representing the accesstoken.</returns>
        public async Task<string> GetAuthTokenAsync(string consumerKey, string consumerSecret, string requestEndPoint)
        {
            string token = await RequestAccessToken(consumerKey, consumerSecret, requestEndPoint);

            return token;
        }



        /// <summary>
        /// Makes a Business to Business payment request between Paybill numbers.
        /// </summary>
        /// <param name="businessToBusinessDto">B2B data transfer object.</param>
        /// <param name="accesstoken">Acccesstoken retrieved by the <c>GetAuthTokenAsync</c> method.</param>
        /// <param name="requestEndPoint">Set to <c>RequestEndPoint.BusinessToBusiness</c></param>
        /// <returns>A JSON string containing data from MPESA API reposnse.</returns>
        public string MakeB2BPayment(BusinessToBusinessDto businessToBusinessDto, string accesstoken, string requestEndPoint)
        {
            return MpesaHttpRequest(businessToBusinessDto, accesstoken, requestEndPoint).GetAwaiter().GetResult();
        }


        /// <summary>
        /// Makes a Business to Business payment request between Paybill numbers.
        /// </summary>
        /// <param name="businessToBusinessDto">B2B data transfer object.</param>
        /// <param name="accesstoken">Acccesstoken retrieved by the <c>GetAuthTokenAsync</c> method.</param>
        /// <param name="requestEndPoint">Set to <c>RequestEndPoint.BusinessToBusiness</c></param>
        /// <returns>A JSON string containing data from MPESA API reposnse.</returns>
        public async Task<string> MakeB2BPaymentAsync(BusinessToBusinessDto businessToBusinessDto, string accesstoken, string requestEndPoint)
        {
            return await MpesaHttpRequest(businessToBusinessDto, accesstoken, requestEndPoint);
        }


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
        public string MakeB2CPayment(BusinessToCustomerDto businessToCustomerDto, string accesstoken, string requestEndPoint)
        {
            return MpesaHttpRequest(businessToCustomerDto, accesstoken, requestEndPoint).GetAwaiter().GetResult();           
        }

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
        public async Task<string> MakeB2CPaymentAsync(BusinessToCustomerDto businessToCustomerDto, string accesstoken, string requestEndPoint)
        {
            return await MpesaHttpRequest(businessToCustomerDto,accesstoken, requestEndPoint);
        }


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
        public string MakeC2BPayment(CustomerToBusinessSimulateDto customerToBusinessSimulateDto, string accesstoken, string requestEndPoint)
        {
            return MpesaHttpRequest(customerToBusinessSimulateDto,accesstoken, requestEndPoint).GetAwaiter().GetResult();           
        }


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
        public async Task<string> MakeC2BPaymentAsync(CustomerToBusinessSimulateDto customerToBusinessSimulateDto, string accesstoken, string requestEndPoint)
        {       
            return await MpesaHttpRequest(customerToBusinessSimulateDto, accesstoken, requestEndPoint);
        }


        /// <summary>
        /// Makes a STK Push payment request to MPESA API Server.
        /// </summary>
        /// <param name="lipaNaMpesaOnlineDto">
        /// Data trnasfer object containing properties for the Lipa Na Mpesa Online API endpoint.
        /// </param>
        /// <param name="accesstoken">Acccesstoken retrieved by the <c>GetAuthTokenAsync</c> method.</param>
        /// <param name="requestEndPoint">Set to <c>RequestEndPoint.LipaNaMpesaOnline</c></param>
        /// <returns>A JSON string containing LNMO response data from MPESA API Server</returns>
        public string MakeLipaNaMpesaOnlinePayment(LipaNaMpesaOnlineDto lipaNaMpesaOnlineDto, string accesstoken, string requestEndPoint)
        {
            return MpesaHttpRequest(lipaNaMpesaOnlineDto, accesstoken, requestEndPoint).GetAwaiter().GetResult();            
        }

        /// <summary>
        /// Makes an STK Push payment request to MPESA API Server.
        /// </summary>
        /// <param name="lipaNaMpesaOnlineDto">
        /// Data transfer object containing properties for the Lipa Na Mpesa Online API endpoint.
        /// </param>
        /// <param name="accesstoken">Acccesstoken retrieved by the <c>GetAuthTokenAsync</c> method.</param>
        /// <param name="requestEndPoint">Set to <c>RequestEndPoint.LipaNaMpesaOnline</c></param>
        /// <returns>A JSON string containing LNMO response data from MPESA API Server</returns>
        public async Task<string> MakeLipaNaMpesaOnlinePaymentAsync(LipaNaMpesaOnlineDto lipaNaMpesaOnlineDto, string accesstoken, string requestEndPoint)
        {         
            return await MpesaHttpRequest(lipaNaMpesaOnlineDto, accesstoken, requestEndPoint);
        }


        /// <summary>
        /// Queries MPESA Paybill Account balance.
        /// </summary>
        /// <param name="accountBalanceQueryDto">Account balance query data transfer object.</param>
        /// <param name="accesstoken">Acccesstoken retrieved by the <c>GetAuthTokenAsync</c> method.</param>
        /// <param name="requestEndPoint">Set to <c>RequestEndPoint.QueryAccountBalance</c></param>
        /// <returns>A JSON string containing data from MPESA API reposnse.</returns>
        public string QueryAccountBalance(AccountBalanceDto accountBalanceQueryDto, string accesstoken, string requestEndPoint)
        {
            return MpesaHttpRequest(accountBalanceQueryDto, accesstoken, requestEndPoint).GetAwaiter().GetResult();
        }


        /// <summary>
        /// Queries MPESA Paybill Account balance.
        /// </summary>
        /// <param name="accountBalanceQueryDto">Account balance query data transfer object.</param>
        /// <param name="accesstoken">Acccesstoken retrieved by the <c>GetAuthTokenAsync</c> method.</param>
        /// <param name="requestEndPoint">Set to <c>RequestEndPoint.QueryAccountBalance</c></param>
        /// <returns>A JSON string containing data from MPESA API reposnse.</returns>
        public async Task<string> QueryAccountBalanceAsync(AccountBalanceDto accountBalanceQueryDto, string accesstoken, string requestEndPoint)
        {        
            return await MpesaHttpRequest(accountBalanceQueryDto,accesstoken, requestEndPoint);
        }


        /// <summary>
        /// Queries Mpesa Online Transaction Status
        /// </summary>
        /// <param name="lipaNaMpesaQueryDto">Transaction Query Data transfer object</param>
        /// <param name="accesstoken">Acccesstoken retrieved by the <c>GetAuthTokenAsync</c> method.</param>
        /// <param name="requestEndPoint">Set to <c>RequestEndPoint.QueryLipaNaMpesaOnlieTransaction</c></param>
        /// <returns>
        /// A JSON string containing data from MPESA API reposnse
        /// </returns>
        /// <remarks>
        /// Use only for transactions initiated with <c>MakeLipaNaMpesaOnlinePayment</c> method.
        /// For Other transaction based methods (C2B,B2C,B2B) use <c>QueryMpesaTransactionStatusAsync</c> method.
        /// </remarks>
        public string QueryLipaNaMpesaTransaction(LipaNaMpesaQueryDto lipaNaMpesaQueryDto, string accesstoken, string requestEndPoint)
        {
            return MpesaHttpRequest(lipaNaMpesaQueryDto, accesstoken, requestEndPoint).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Queries Mpesa Online Transaction Status
        /// </summary>
        /// <param name="lipaNaMpesaQueryDto">Transaction Query Data transfer object</param>
        /// <param name="accesstoken">Acccesstoken retrieved by the <c>GetAuthTokenAsync</c> method.</param>
        /// <param name="requestEndPoint">Set to <c>RequestEndPoint.QueryLipaNaMpesaOnlieTransaction</c></param>
        /// <returns>
        /// A JSON string containing data from MPESA API reposnse
        /// </returns>
        /// <remarks>
        /// Use only for transactions initiated with <c>MakeLipaNaMpesaOnlinePayment</c> method.
        /// For Other transaction based methods (C2B,B2C,B2B) use <c>QueryMpesaTransactionStatusAsync</c> method.
        /// </remarks>
        public async Task<string> QueryLipaNaMpesaTransactionAsync(LipaNaMpesaQueryDto lipaNaMpesaQueryDto, string accesstoken, string requestEndPoint)
        {          
            return await MpesaHttpRequest(lipaNaMpesaQueryDto,accesstoken, requestEndPoint);
        }


        /// <summary>
        /// Queries status of an Mpesa transaction
        /// </summary>
        /// <param name="mpesaTransactionStatusDto"></param>
        /// <param name="accesstoken"></param>
        /// <param name="requestEndPoint"></param>
        /// <returns></returns>
        public string QueryMpesaTransactionStatus(MpesaTransactionStatusDto mpesaTransactionStatusDto, string accesstoken, string requestEndPoint)
        {         
            return MpesaHttpRequest(mpesaTransactionStatusDto,accesstoken, requestEndPoint).GetAwaiter().GetResult();           
        }

        /// <summary>
        /// Queries status of an Mpesa transaction
        /// </summary>
        /// <param name="mpesaTransactionStatusDto"></param>
        /// <param name="accesstoken"></param>
        /// <param name="requestEndPoint"></param>
        /// <returns></returns>
        public async Task<string> QueryMpesaTransactionStatusAsync(MpesaTransactionStatusDto mpesaTransactionStatusDto, string accesstoken, string requestEndPoint)
        {        
            return await MpesaHttpRequest(mpesaTransactionStatusDto,accesstoken, requestEndPoint);
        }


        /// <summary>
        /// Registers Customer to Business Confirmation and Validation URLs.
        /// </summary>
        /// <param name="customerToBusinessRegisterUrlDto">C2B Register URLs data transfer object.</param>
        /// <param name="accesstoken">Acccesstoken retrieved by the <c>GetAuthTokenAsync</c> method.</param>
        /// <param name="requestEndPoint">Set to <c>RequestEndPoint.RegisterC2BUrl</c></param>
        /// <returns>A JSON string containing data from MPESA API reposnse.</returns>
        public string RegisterC2BUrl(CustomerToBusinessRegisterUrlDto customerToBusinessRegisterUrlDto, string accesstoken, string requestEndPoint)
        {          
            return MpesaHttpRequest(customerToBusinessRegisterUrlDto,accesstoken, requestEndPoint).GetAwaiter().GetResult();            
        }


        /// <summary>
        /// Registers Customer to Business Confirmation and Validation URLs.
        /// </summary>
        /// <param name="customerToBusinessRegisterUrlDto">C2B Register URLs data transfer object.</param>
        /// <param name="accesstoken">Acccesstoken retrieved by the <c>GetAuthTokenAsync</c> method.</param>
        /// <param name="requestEndPoint">Set to <c>RequestEndPoint.RegisterC2BUrl</c></param>
        /// <returns>A JSON string containing data from MPESA API reposnse.</returns>
        public async Task<string> RegisterC2BUrlAsync(CustomerToBusinessRegisterUrlDto customerToBusinessRegisterUrlDto, string accesstoken, string requestEndPoint)
        {           
            return await MpesaHttpRequest(customerToBusinessRegisterUrlDto,accesstoken, requestEndPoint);
        }


        /// <summary>
        /// Reverses a B2B, B2C or C2B M-Pesa transaction.
        /// </summary>
        /// <param name="reversalDto">Reversal data transfer object.</param>
        /// <param name="accesstoken">Acccesstoken retrieved by the <c>GetAuthTokenAsync</c> method.</param>
        /// <param name="requestEndPoint">Set to <c>RequestEndPoint.ReverseMpesaTransaction</c></param>
        /// <returns>A JSON string containing data from MPESA API reposnse.</returns>
        public string ReverseMpesaTransaction(ReversalDto reversalDto, string accesstoken, string requestEndPoint)
        {
            return  MpesaHttpRequest(reversalDto, accesstoken, requestEndPoint).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Reverses a B2B, B2C or C2B M-Pesa transaction.
        /// </summary>
        /// <param name="reversalDto">Reversal data transfer object.</param>
        /// <param name="accesstoken">Acccesstoken retrieved by the <c>GetAuthTokenAsync</c> method.</param>
        /// <param name="requestEndPoint">Set to <c>RequestEndPoint.ReverseMpesaTransaction</c></param>
        /// <returns>A JSON string containing data from MPESA API reposnse.</returns>
        public async Task<string> ReverseMpesaTransactionAsync(ReversalDto reversalDto, string accesstoken, string requestEndPoint)
        {                     
            return await MpesaHttpRequest(reversalDto,accesstoken, requestEndPoint);
        }


        /// <summary>
        /// Initializes the Httpclient for each handler
        /// </summary>
        /// <param name="httpclient">httpclient instance</param>
        /// <param name="accesstoken">accesstoken</param>
        private static void HttpClientInit(HttpClient httpclient, string accesstoken)
        {
            httpclient.DefaultRequestHeaders.Clear();
            httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);
        }



        /// <summary>
        /// Method makes the accesstoken request to mpesa api
        /// </summary>
        /// <param name="consumerKey"></param>
        /// <param name="consumerSecret"></param>
        /// <param name="requestEndPoint"></param>
        /// <returns>string representing accesstoken</returns>
        private async Task<string> RequestAccessToken(string consumerKey, string consumerSecret, string requestEndPoint)
        {
            _httpclient.DefaultRequestHeaders.Clear();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestEndPoint);

            var keyBytes = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{consumerKey}:{consumerSecret}"));

            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", keyBytes);

            var response = await _httpclient.SendAsync(request);            

            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode == false)
            {
                throw new MpesaApiException
                {
                    StatusCode = (int)response.StatusCode,
                    Content = content
                };
            }

            return JsonConvert.DeserializeObject<TokenResponse>(content).AccessToken;
        }

        /// <summary>
        /// Makes HttpRequest to mpesa api server
        /// </summary>
        /// <param name="Dto">Data transfer object</param>
        /// <param name="token">Mpesa Accesstoken</param>
        /// <param name="Endpoint">Request endpoint</param>        
        /// <returns>Mpesa API response</returns>
        private async Task<string> MpesaHttpRequest(object Dto,string token, string Endpoint)
        {
            HttpClientInit(_httpclient, token);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Endpoint)
            {
                Content = new StringContent(JsonConvert.SerializeObject(Dto).ToString(), Encoding.UTF8, "application/json")
            };

            HttpResponseMessage response = await _httpclient.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode == false)
            {
                throw new MpesaApiException
                {
                    StatusCode = (int)response.StatusCode,
                    Content = content
                };
            }

            return content;
        }


    }
}
