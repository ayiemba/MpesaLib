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
    public class C2BSimulateClient : IC2BSimulateClient
    {
        private readonly HttpClient _httpclient;
        public C2BSimulateClient(HttpClient httpClient)
        {
            _httpclient = httpClient;
        }

        public async Task<string> PostData(CustomerToBusinessSimulate c2bsimulate, string accesstoken)
        {            

            var BaseAddress = new Uri("https://sandbox.safaricom.co.ke/mpesa/c2b/v1/simulate");

           // client.DefaultRequestHeaders.Accept.Clear();
            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //_httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", "Bearer " + accesstoken);

            var values = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("ShortCode", c2bsimulate.ShortCode),
                new KeyValuePair<string, string>("CommandID", c2bsimulate.CommandID),
                new KeyValuePair<string, string>("Amount", c2bsimulate.Amount),
                new KeyValuePair<string, string>("Msisdn", c2bsimulate.Msisdn),
                new KeyValuePair<string, string>("BillRefNumber", c2bsimulate.BillRefNumber)

            };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, BaseAddress)
            {
                Content = new FormUrlEncodedContent(values)
            };

            request.Headers.Authorization = new AuthenticationHeaderValue("Authorization", "Bearer " + accesstoken);
            request.Headers.Host = "sandbox.safaricom.co.ke";


            HttpResponseMessage response = await _httpclient.SendAsync(request);

            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
