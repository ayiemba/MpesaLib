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
    public class LipaNaMpesaOnlineClient : ILipaNaMpesaOnlineClient
    {
        private readonly HttpClient _httpclient;
        public LipaNaMpesaOnlineClient(HttpClient httpClient)
        {
            _httpclient = httpClient;
        }

        public async Task<string> MakePayment(LipaNaMpesaOnline mpesaItem, string accesstoken)
        {
            var BaseAddress = new Uri("https://sandbox.safaricom.co.ke/mpesa/stkpush/v1/processrequest");

            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);           

            var values = new Dictionary<string, string>
            {
                { "BusinessShortCode", mpesaItem.BusinessShortCode },
                { "Password", mpesaItem.Password },
                { "Timestamp", mpesaItem.Timestamp },
                { "TransactionType", mpesaItem.TransactionType },
                { "Amount", mpesaItem.Amount },
                { "PartyA", mpesaItem.PartyA },
                { "PartyB", mpesaItem.PartyB },
                { "PhoneNumber", mpesaItem.PhoneNumber },
                { "CallBackURL", mpesaItem.CallBackURL },
                { "AccountReference", mpesaItem.AccountReference },
                { "TransactionDesc", mpesaItem.TransactionDesc }
            };
           
            var jsonvalues = JsonConvert.SerializeObject(values);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, BaseAddress)
            {
                Content = new StringContent(jsonvalues.ToString(), Encoding.UTF8, "application/json")
            };

            HttpResponseMessage response = await _httpclient.SendAsync(request);

            Console.WriteLine("This is the request data: " + jsonvalues.ToString());

            return response.Content.ReadAsStringAsync().Result;

        }
    }
}
