using MpesaLib.Interfaces;
using MpesaLib.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MpesaLib.Clients
{
    /// <summary>
    /// Instantiate this class to generate an accesstoken to use with the other Mpesa API clients
    /// </summary>
    public class AuthClient : IAuthClient
    {
        private readonly HttpClient _httpclient;

        /// <summary>
        /// Returns accesstoken
        /// </summary>
        /// <param name="httpClient"></param>
        public AuthClient(HttpClient httpClient)
        {
            _httpclient = httpClient;           
        }

        /// <summary>
        /// Defaults to sandbox URL. Override with Mpesa production url when deploying to a production environment.
        /// </summary>
        public string BaseUri { get; set; } = "https://sandbox.safaricom.co.ke/oauth/v1/generate?grant_type=client_credentials";

        /// <summary>
        /// Make an accesstoken request
        /// </summary>
        /// <param name="consumerKey">Your Mpesa Api ConsumerKey from daraja/safaricom</param>
        /// <param name="consumerSecret">Your Mpesa Api ConsumerSecret from daraja/safaricom</param>
        /// <returns>Returns acccesstoken only</returns>
        public async Task<string> GetToken(string consumerKey, string consumerSecret)
        {
            Uri BaseAddress = new Uri(BaseUri);

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
