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

            //_httpclient.DefaultRequestHeaders.Accept.Clear();
            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);
            //_httpclient.BaseAddress = BaseAddress;
            //_httpclient.DefaultRequestHeaders.Host = "sandbox.safaricom.co.ke";

            //var values = new List<KeyValuePair<string, string>>
            //{
            //    new KeyValuePair<string, string>("BusinessShortCode", mpesaItem.BusinessShortCode),
            //    new KeyValuePair<string, string>("Password", mpesaItem.Password),
            //    new KeyValuePair<string, string>("Timestamp", mpesaItem.Timestamp),
            //    new KeyValuePair<string, string>("TransactionType", mpesaItem.TransactionType),
            //    new KeyValuePair<string, string>("Amount", mpesaItem.Amount),
            //    new KeyValuePair<string, string>("PartyA", mpesaItem.PartyA),
            //    new KeyValuePair<string, string>("PartyB", mpesaItem.PartyB),
            //    new KeyValuePair<string, string>("PhoneNumber", mpesaItem.PhoneNumber),
            //    new KeyValuePair<string, string>("CallBackURL", mpesaItem.CallBackURL),
            //    new KeyValuePair<string, string>("AccountReference", mpesaItem.AccountReference),
            //    new KeyValuePair<string, string>("TransactionDesc", mpesaItem.TransactionDesc)
            //};

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
                //Content = new FormUrlEncodedContent(values)
                Content = new StringContent(jsonvalues.ToString(), Encoding.UTF8, "application/json")
            };

            HttpResponseMessage response = await _httpclient.SendAsync(request);

            Console.WriteLine("This is the request data: " + jsonvalues.ToString());

            return response.Content.ReadAsStringAsync().Result;

        }
    }
}
