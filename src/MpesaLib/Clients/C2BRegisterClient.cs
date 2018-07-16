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
    public class C2BRegisterClient : IC2BRegisterClient
    {
        private readonly HttpClient _httpclient;
        public C2BRegisterClient(HttpClient httpClient)
        {
            _httpclient = httpClient;
        }
        public async Task<string> GetData(CustomerToBusinessRegister c2bregisterItem, string accesstoken)
        {
            var BaseAddress = new Uri("https://sandbox.safaricom.co.ke/mpesa/c2b/v1/registerurl");
          
            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);

            

            var values = new Dictionary<string, string>
            {
                {"ShortCode", c2bregisterItem.ShortCode },
                {"ResponseType", c2bregisterItem.ResponseType },
                {"ConfirmationURL", c2bregisterItem.ConfirmationURL },
                { "ValidationURL", c2bregisterItem.ValidationURL }

            };


            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, BaseAddress)
            {
                Content = new StringContent(JsonConvert.SerializeObject(values).ToString(), Encoding.UTF8, "application/json")
            };

            //request.Headers.Authorization = new AuthenticationHeaderValue("Authorization", "Bearer " + accesstoken);
            //request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //request.Headers.Host = "sandbox.safaricom.co.ke";

            HttpResponseMessage response = await _httpclient.SendAsync(request);

            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
