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

        public async Task<string> GetData(string consumerKey, string consumerSecret)
        {
            var BaseAddress = new Uri("https://sandbox.safaricom.co.ke/oauth/v1/generate?grant_type=client_credentials");

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, BaseAddress);

            var keyBytes = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{consumerKey}:{consumerSecret}")); 

            //var keyBytes2 = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
            //    .GetBytes($"{consumerKey}:{consumerSecret}"));

            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", keyBytes);
            request.Headers.Host = "sandbox.safaricom.co.ke";

            HttpResponseMessage response = await _httpclient.SendAsync(request);

            var content = response.Content;

            var token = JsonConvert.DeserializeObject<Token>(content.ReadAsStringAsync().Result);

            return token.access_token;

        }
    }
}
