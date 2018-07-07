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
    public class B2CClient : IB2CClient
    {
        private readonly HttpClient _httpclient;
        public B2CClient(HttpClient httpClient)
        {
            _httpclient = httpClient;
        }
        public async Task<string> PostData(BusinessToCustomer b2citem, string accesstoken)
        {
            var BaseAddress = new Uri("https://sandbox.safaricom.co.ke/mpesa/b2c/v1/paymentrequest");

            _httpclient.DefaultRequestHeaders.Accept.Clear();
            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));     

            var values = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("InitiatorName", b2citem.InitiatorName),
                new KeyValuePair<string, string>("SecurityCredential", b2citem.SecurityCredential),
                new KeyValuePair<string, string>("CommandID", b2citem.CommandID),
                new KeyValuePair<string, string>("Amount", b2citem.Amount),
                new KeyValuePair<string, string>("PartyA", b2citem.PartyA),
                new KeyValuePair<string, string>("PartyB", b2citem.PartyB),
                new KeyValuePair<string, string>("Remarks", b2citem.Remarks),
                new KeyValuePair<string, string>("QueueTimeOutURL", b2citem.QueueTimeOutURL),
                new KeyValuePair<string, string>("ResultURL", b2citem.ResultURL),
                new KeyValuePair<string, string>("Occasion", b2citem.Occasion)
            };

            var request = new HttpRequestMessage(HttpMethod.Post, BaseAddress)
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
