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
    public class AuthClient : IAuthClient
    {
        private readonly HttpClient _httpclient;      

        public AuthClient(HttpClient httpClient)
        {
            _httpclient = httpClient;           
        }

        public Uri BaseAddress { get; set; } = new Uri("https://sandbox.safaricom.co.ke/oauth/v1/generate?grant_type=client_credentials");

        public async Task<string> GetToken(string consumerKey, string consumerSecret)
        {          
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, BaseAddress);

            var keyBytes = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{consumerKey}:{consumerSecret}"));

            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", keyBytes);            

            HttpResponseMessage response = await _httpclient.SendAsync(request);

            var content = response.Content;

            var token = JsonConvert.DeserializeObject<Token>(content.ReadAsStringAsync().Result);

            return token.access_token;

        }
    }
}
