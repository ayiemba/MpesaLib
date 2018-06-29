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
    public class LipaNaMpesaQueryClient : ILipaNaMpesaQueryClient
    {
        private readonly HttpClient _httpclient;
        public LipaNaMpesaQueryClient(HttpClient httpClient)
        {
            _httpclient = httpClient;
        }

        public async Task<string> GetData(LipaNaMpesaQuery mpesaQuery, string accesstoken)
        {
            var BaseAddress = new Uri("https://sandbox.safaricom.co.ke/mpesa/stkpushquery/v1/query");

            _httpclient.DefaultRequestHeaders.Accept.Clear();
            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //_httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", "Bearer " + accesstoken);

            var values = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("BusinessShortCode", mpesaQuery.BusinessShortCode),
                new KeyValuePair<string, string>("Password", mpesaQuery.Password),
                new KeyValuePair<string, string>("Timestamp", mpesaQuery.Timestamp),
                new KeyValuePair<string, string>("CheckoutRequestID", mpesaQuery.CheckoutRequestID)

            };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, BaseAddress)
            {
                Content = new FormUrlEncodedContent(values)
            };

            request.Headers.Authorization = new AuthenticationHeaderValue("Authorization", "Bearer " + accesstoken);


            HttpResponseMessage response = await _httpclient.SendAsync(request);

            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
