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
           
            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);


            var values = new Dictionary<string, string>
            {
                {"ShortCode", c2bsimulate.ShortCode },
                {"CommandID", c2bsimulate.CommandID },
                {"Amount", c2bsimulate.Amount },
                {"Msisdn", c2bsimulate.Msisdn },
                { "BillRefNumber", c2bsimulate.BillRefNumber }

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
