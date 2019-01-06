using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
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
            _httpclient.DefaultRequestHeaders.Clear();           

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestEndPoint);

            var keyBytes = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{consumerKey}:{consumerSecret}"));
            
            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", keyBytes);
            
            var response = await _httpclient.SendAsync(request);           

            response.EnsureSuccessStatusCode();

            var content = response.Content;

            var token = JsonConvert.DeserializeObject<TokenDto>(content.ReadAsStringAsync().Result);

            return token.access_token;
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
            throw new NotImplementedException();
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

            _httpclient.DefaultRequestHeaders.Clear();           
            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);

            var values = new Dictionary<string, string>
            {
                { "Initiator", businessToBusinessDto.Initiator },
                { "SecurityCredential", businessToBusinessDto.SecurityCredential },
                {"CommandID", businessToBusinessDto.CommandID },
                {"SenderIdentifierType", businessToBusinessDto.SenderIdentifierType },
                {"RecieverIdentifierType", businessToBusinessDto.RecieverIdentifierType },
                {"Amount", businessToBusinessDto.Amount },
                {"PartyA", businessToBusinessDto.PartyA },
                {"PartyB", businessToBusinessDto.PartyB },
                {"AccountReference", businessToBusinessDto.AccountReference },
                {"Remarks", businessToBusinessDto.Remarks },
                { "QueueTimeOutURL", businessToBusinessDto.QueueTimeOutURL},
                { "ResultURL", businessToBusinessDto.ResultURL }
            };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestEndPoint)
            {
                Content = new StringContent(JsonConvert.SerializeObject(values).ToString(), Encoding.UTF8, "application/json")
            };
           
            var  response = await _httpclient.SendAsync(request);            

            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsStringAsync().Result;
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
            throw new NotImplementedException();
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
            
            _httpclient.DefaultRequestHeaders.Clear();           
            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);

            var values = new Dictionary<string, string>
            {
                {"InitiatorName", businessToCustomerDto.InitiatorName },
                {"SecurityCredential", businessToCustomerDto.SecurityCredential },
                {"CommandID", businessToCustomerDto.CommandID },
                {"Amount", businessToCustomerDto.Amount },
                {"PartyA", businessToCustomerDto.PartyA },
                {"PartyB", businessToCustomerDto.PartyB },
                {"Remarks", businessToCustomerDto.Remarks },
                {"QueueTimeOutURL", businessToCustomerDto.QueueTimeOutURL },
                {"ResultURL", businessToCustomerDto.ResultURL },
                { "Occasion", businessToCustomerDto.Occasion }
            };


            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestEndPoint)
            {
                Content = new StringContent(JsonConvert.SerializeObject(values).ToString(), Encoding.UTF8, "application/json")
            };
            
            var  response = await _httpclient.SendAsync(request);            

            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsStringAsync().Result;
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
            throw new NotImplementedException();
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
            _httpclient.DefaultRequestHeaders.Clear();          
            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);

            var values = new Dictionary<string, string>
            {
                {"ShortCode", customerToBusinessSimulateDto.ShortCode },
                {"CommandID", customerToBusinessSimulateDto.CommandID },
                {"Amount", customerToBusinessSimulateDto.Amount },
                {"Msisdn", customerToBusinessSimulateDto.Msisdn },
                { "BillRefNumber", customerToBusinessSimulateDto.BillRefNumber }

            };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestEndPoint)
            {
                Content = new StringContent(JsonConvert.SerializeObject(values).ToString(), Encoding.UTF8, "application/json")
            };
           
            var  response = await _httpclient.SendAsync(request);            

            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsStringAsync().Result;
        }


        /// <summary>
        /// Makes an STK Push payment request to MPESA API Server.
        /// </summary>
        /// <param name="lipaNaMpesaOnlineDto">
        /// Data trnasfer object containing properties for the Lipa Na Mpesa Online API endpoint.
        /// </param>
        /// <param name="accesstoken">Acccesstoken retrieved by the <c>GetAuthTokenAsync</c> method.</param>
        /// <param name="requestEndPoint">Set to <c>RequestEndPoint.LipaNaMpesaOnline</c></param>
        /// <returns>A JSON string containing LNMO response data from MPESA API Server</returns>
        public string MakeLipaNaMpesaOnlinePayment(LipaNaMpesaOnlineDto lipaNaMpesaOnlineDto, string accesstoken, string requestEndPoint)
        {
            throw new NotImplementedException();
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

/*#if NET45
            ServicePointManager.SecurityProtocol =
                         SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
#endif*/
            _httpclient.DefaultRequestHeaders.Clear();           
            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);

            var values = new Dictionary<string, string>
            {
                { "BusinessShortCode", lipaNaMpesaOnlineDto.BusinessShortCode },
                { "Password", lipaNaMpesaOnlineDto.Password },
                { "Timestamp", lipaNaMpesaOnlineDto.Timestamp },
                { "TransactionType", lipaNaMpesaOnlineDto.TransactionType },
                { "Amount", lipaNaMpesaOnlineDto.Amount },
                { "PartyA", lipaNaMpesaOnlineDto.PartyA },
                { "PartyB", lipaNaMpesaOnlineDto.PartyB },
                { "PhoneNumber", lipaNaMpesaOnlineDto.PhoneNumber },
                { "CallBackURL", lipaNaMpesaOnlineDto.CallBackURL },
                { "AccountReference", lipaNaMpesaOnlineDto.AccountReference },
                { "TransactionDesc", lipaNaMpesaOnlineDto.TransactionDesc }
            };

            var jsonvalues = JsonConvert.SerializeObject(values);

            //var policy = CreateTokenRefreshPolicy(tokenRefreshed);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestEndPoint)
            {
                Content = new StringContent(jsonvalues.ToString(), Encoding.UTF8, "application/json")
            };          
            
            var response = await _httpclient.SendAsync(request);         

            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsStringAsync().Result;
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
            throw new NotImplementedException();
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


            _httpclient.DefaultRequestHeaders.Clear();          
            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);

            var values = new Dictionary<string, string>
            {
                { "Initiator", accountBalanceQueryDto.Initiator },
                { "SecurityCredential", accountBalanceQueryDto.SecurityCredential},
                { "CommandID", accountBalanceQueryDto.CommandID},
                { "PartyA", accountBalanceQueryDto.PartyA},
                { "IdentifierType", accountBalanceQueryDto.IdentifierType},
                { "Remarks", accountBalanceQueryDto.Remarks },
                { "QueueTimeOutURL", accountBalanceQueryDto.QueueTimeOutURL},
                { "ResultURL", accountBalanceQueryDto.ResultURL}
            };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestEndPoint)
            {
                Content = new StringContent(JsonConvert.SerializeObject(values).ToString(), Encoding.UTF8, "application/json")
            };
           
            var  response = await _httpclient.SendAsync(request);            

            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsStringAsync().Result;
        }


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
        public string QueryLipaNaMpesaTransaction(LipaNaMpesaQueryDto mpesaTransactionQueryDto, string accesstoken, string requestEndPoint)
        {
            throw new NotImplementedException();
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
            _httpclient.DefaultRequestHeaders.Clear();          
            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);

            var values = new Dictionary<string, string>
            {
                {"BusinessShortCode", lipaNaMpesaQueryDto.BusinessShortCode },
                {"Password", lipaNaMpesaQueryDto.Password },
                {"Timestamp", lipaNaMpesaQueryDto.Timestamp },
                { "CheckoutRequestID", lipaNaMpesaQueryDto.CheckoutRequestID }

            };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestEndPoint)
            {
                Content = new StringContent(JsonConvert.SerializeObject(values).ToString(), Encoding.UTF8, "application/json")
            };
           
            var  response = await _httpclient.SendAsync(request);           

            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsStringAsync().Result;
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
            throw new NotImplementedException();
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

            _httpclient.DefaultRequestHeaders.Clear();           
            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);

            var values = new Dictionary<string, string>
            {
                {"Initiator", mpesaTransactionStatusDto.Initiator },
                {"SecurityCredential", mpesaTransactionStatusDto.SecurityCredential },
                {"CommandID", mpesaTransactionStatusDto.CommandID },
                {"TransactionID", mpesaTransactionStatusDto.TransactionID },
                {"PartyA", mpesaTransactionStatusDto.PartyA },
                {"IdentifierType", mpesaTransactionStatusDto.IdentifierType },
                {"ResultURL", mpesaTransactionStatusDto.ResultURL },
                {"QueueTimeOutURL", mpesaTransactionStatusDto.QueueTimeOutURL },
                {"Remarks", mpesaTransactionStatusDto.Remarks },
                { "Occasion", mpesaTransactionStatusDto.Occasion }

            };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestEndPoint)
            {
                Content = new StringContent(JsonConvert.SerializeObject(values).ToString(), Encoding.UTF8, "application/json")
            };
           
            var response = await _httpclient.SendAsync(request);           

            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsStringAsync().Result;
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
            throw new NotImplementedException();
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

            _httpclient.DefaultRequestHeaders.Clear();            
            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);

            var values = new Dictionary<string, string>
            {
                {"ShortCode", customerToBusinessRegisterUrlDto.ShortCode },
                {"ResponseType", customerToBusinessRegisterUrlDto.ResponseType },
                {"ConfirmationURL", customerToBusinessRegisterUrlDto.ConfirmationURL },
                { "ValidationURL", customerToBusinessRegisterUrlDto.ValidationURL }

            };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestEndPoint)
            {
                Content = new StringContent(JsonConvert.SerializeObject(values).ToString(), Encoding.UTF8, "application/json")
            };
          
            var response = await _httpclient.SendAsync(request);           

            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsStringAsync().Result;
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
            throw new NotImplementedException();
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
            _httpclient.DefaultRequestHeaders.Clear();           
            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);

            var values = new Dictionary<string, string>
            {
                {"Initiator", reversalDto.Initiator },
                {"SecurityCredential", reversalDto.SecurityCredential },
                {"CommandID", reversalDto.CommandID },
                {"TransactionID", reversalDto.TransactionID },                
                {"ReceiverParty", reversalDto.ReceiverParty },
                {"RecieverIdentifierType", reversalDto.RecieverIdentifierType },
                {"ResultURL", reversalDto.ResultURL },
                {"QueueTimeOutURL", reversalDto.QueueTimeOutURL },
                {"Remarks", reversalDto.Remarks },
                { "Occasion", reversalDto.Occasion }

            };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestEndPoint)
            {
                Content = new StringContent(JsonConvert.SerializeObject(values).ToString(), Encoding.UTF8, "application/json")
            };
            
            var response = await _httpclient.SendAsync(request);           

            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsStringAsync().Result;
        }






    }
}
