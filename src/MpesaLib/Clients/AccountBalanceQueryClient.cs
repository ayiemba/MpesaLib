using MpesaLib.Interfaces;
using MpesaLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MpesaLib.Clients
{
    public class AccountBalanceQueryClient : IAccountBalanceQueryClient
    {
        private readonly HttpClient _httpClient;

        public AccountBalanceQueryClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Uri BaseAddress { get; set; } = new Uri("https://sandbox.safaricom.co.ke/mpesa/accountbalance/v1/query");

        public async Task<string> GetBalance(AccountBalance accbalance, string accesstoken)
        {                       
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",accesstoken);                   

            var values = new Dictionary<string, string>
            {
                { "Initiator", accbalance.Initiator },
                { "SecurityCredential", accbalance.SecurityCredential},
                { "CommandID", accbalance.CommandID},
                { "PartyA", accbalance.PartyA},
                { "IdentifierType", accbalance.IdentifierType},
                { "Remarks", accbalance.Remarks },
                { "QueueTimeOutURL", accbalance.QueueTimeOutURL},
                { "ResultURL", accbalance.ResultURL}
            };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, BaseAddress)
            {
                Content = new StringContent(JsonConvert.SerializeObject(values).ToString(), Encoding.UTF8, "application/json")
            };


            HttpResponseMessage response = await _httpClient.SendAsync(request);

            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
