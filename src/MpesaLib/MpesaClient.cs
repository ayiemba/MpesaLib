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
        /// This methods returns an accesstoken to be passed into the other methods
        /// </summary>
        /// <param name="consumerKey"></param>
        /// <param name="consumerSecret"></param>
        /// <param name="requestEndPoint"></param>
        /// <returns></returns>
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
        /// Makes a Business to Business payment
        /// </summary>
        /// <param name="BusinessToBusinessDto"></param>
        /// <param name="accesstoken"></param>
        /// <param name="requestEndPoint"></param>
        /// <returns></returns>
        public async Task<string> MakeB2BPaymentAsync(BusinessToBusinessDto BusinessToBusinessDto, string accesstoken, string requestEndPoint)
        {

            _httpclient.DefaultRequestHeaders.Clear();           
            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);

            var values = new Dictionary<string, string>
            {
                { "Initiator", BusinessToBusinessDto.Initiator },
                { "SecurityCredential", BusinessToBusinessDto.SecurityCredential },
                {"CommandID", BusinessToBusinessDto.CommandID },
                {"SenderIdentifierType", BusinessToBusinessDto.SenderIdentifierType },
                {"RecieverIdentifierType", BusinessToBusinessDto.RecieverIdentifierType },
                {"Amount", BusinessToBusinessDto.Amount },
                {"PartyA", BusinessToBusinessDto.PartyA },
                {"PartyB", BusinessToBusinessDto.PartyB },
                {"AccountReference", BusinessToBusinessDto.AccountReference },
                {"Remarks", BusinessToBusinessDto.Remarks },
                { "QueueTimeOutURL", BusinessToBusinessDto.QueueTimeOutURL},
                { "ResultURL", BusinessToBusinessDto.ResultURL }
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
        /// Makes a Business to Customer payment
        /// </summary>
        /// <param name="businessToCustomerDto"></param>
        /// <param name="accesstoken"></param>
        /// <param name="requestEndPoint"></param>
        /// <returns></returns>
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
        /// Makes a Customer to business payment - Customer initiates transaction
        /// </summary>
        /// <param name="customerToBusinessSimulateDto"></param>
        /// <param name="accesstoken"></param>
        /// <param name="requestEndPoint"></param>
        /// <returns></returns>
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
        /// Makes a customer to business payment - system or business initates transaction through stk push, customer completes transaction
        /// </summary>
        /// <param name="lipaNaMpesaOnlineDto"></param>
        /// <param name="accesstoken"></param>
        /// <param name="requestEndPoint"></param>        
        /// <returns></returns>
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
        /// Queries business Mpesa account balance
        /// </summary>
        /// <param name="accountBalanceDto"></param>
        /// <param name="accesstoken"></param>
        /// <param name="requestEndPoint"></param>
        /// <returns></returns>
        public async Task<string> QueryAccountBalanceAsync(AccountBalanceDto accountBalanceDto, string accesstoken, string requestEndPoint)
        {


            _httpclient.DefaultRequestHeaders.Clear();          
            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);

            var values = new Dictionary<string, string>
            {
                { "Initiator", accountBalanceDto.Initiator },
                { "SecurityCredential", accountBalanceDto.SecurityCredential},
                { "CommandID", accountBalanceDto.CommandID},
                { "PartyA", accountBalanceDto.PartyA},
                { "IdentifierType", accountBalanceDto.IdentifierType},
                { "Remarks", accountBalanceDto.Remarks },
                { "QueueTimeOutURL", accountBalanceDto.QueueTimeOutURL},
                { "ResultURL", accountBalanceDto.ResultURL}
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
        /// Queries status of a LipaNaMpesa online transaction
        /// </summary>
        /// <param name="lipaNaMpesaQueryDto"></param>
        /// <param name="accesstoken"></param>
        /// <param name="requestEndPoint"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="customerToBusinessRegisterUrlDto"></param>
        /// <param name="accesstoken"></param>
        /// <param name="requestEndPoint"></param>
        /// <returns></returns>
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
        /// Reverse Mpesa Transaction
        /// </summary>
        /// <param name="reversalDto"></param>
        /// <param name="accesstoken"></param>
        /// <param name="requestEndPoint"></param>
        /// <returns></returns>
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
                {"Amount", reversalDto.Amount },
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
