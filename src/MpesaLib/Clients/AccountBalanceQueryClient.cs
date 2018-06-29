using MpesaLib.Interfaces;
using MpesaLib.Models;
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
        public async Task<string> GetData(AccountBalance accbalance, string accesstoken)
        {
            var BaseAddress = new Uri("https://sandbox.safaricom.co.ke/mpesa/accountbalance/v1/query");

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var values = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Initiator", accbalance.Initiator),
                new KeyValuePair<string, string>("SecurityCredential", accbalance.SecurityCredential),
                new KeyValuePair<string, string>("CommandID", accbalance.CommandID),
                new KeyValuePair<string, string>("PartyA", accbalance.PartyA),
                new KeyValuePair<string, string>("IdentifierType", accbalance.IdentifierType),
                new KeyValuePair<string, string>("Remarks", accbalance.Remarks),
                new KeyValuePair<string, string>("QueueTimeOutURL", accbalance.QueueTimeOutURL),
                new KeyValuePair<string, string>("ResultURL", accbalance.ResultURL)
            };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, BaseAddress)
            {
                Content = new FormUrlEncodedContent(values)
            };

            request.Headers.Authorization = new AuthenticationHeaderValue("Authorization", "Bearer " + accesstoken);


            HttpResponseMessage response = await _httpClient.SendAsync(request);

            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
