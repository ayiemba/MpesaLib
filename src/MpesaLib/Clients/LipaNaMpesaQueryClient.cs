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
    public class LipaNaMpesaQueryClient : ILipaNaMpesaQueryClient
    {
        private readonly HttpClient _httpclient;
        public LipaNaMpesaQueryClient(HttpClient httpClient)
        {
            _httpclient = httpClient;
        }

        public Uri BaseAddress { get; set; } = new Uri("https://sandbox.safaricom.co.ke/mpesa/stkpushquery/v1/query");

        public async Task<string> MakeMpesaQuery(LipaNaMpesaQuery mpesaQuery, string accesstoken)
        {           

            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);

            var values = new Dictionary<string, string>
            {
                {"BusinessShortCode", mpesaQuery.BusinessShortCode },
                {"Password", mpesaQuery.Password },
                {"Timestamp", mpesaQuery.Timestamp },
                { "CheckoutRequestID", mpesaQuery.CheckoutRequestID }

            };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, BaseAddress)
            {
                Content = new StringContent(JsonConvert.SerializeObject(values).ToString(), Encoding.UTF8, "application/json")
            };


            HttpResponseMessage response = await _httpclient.SendAsync(request);

            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
