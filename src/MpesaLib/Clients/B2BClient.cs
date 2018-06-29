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
    public class B2BClient : IB2BClient
    {
        private readonly HttpClient _httpclient;
        public B2BClient(HttpClient httpClient)
        {
            _httpclient = httpClient;
        }

        public async Task<string> PostData(BusinessToBusiness b2bitem, string token)
        {
            var BaseAddress = new Uri("https://sandbox.safaricom.co.ke/mpesa/b2b/v1/paymentrequest");
            _httpclient.DefaultRequestHeaders.Accept.Clear();
            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));           

            var values = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Initiator", b2bitem.Initiator),
                new KeyValuePair<string, string>("SecurityCredential", b2bitem.SecurityCredential),
                new KeyValuePair<string, string>("CommandID", b2bitem.CommandID),
                new KeyValuePair<string, string>("SenderIdentifierType", b2bitem.SenderIdentifierType),
                new KeyValuePair<string, string>("RecieverIdentifierType", b2bitem.RecieverIdentifierType),
                new KeyValuePair<string, string>("Amount", b2bitem.Amount),
                new KeyValuePair<string, string>("PartyA", b2bitem.PartyA),
                new KeyValuePair<string, string>("PartyB", b2bitem.PartyB),
                new KeyValuePair<string, string>("AccountReference", b2bitem.AccountReference),
                new KeyValuePair<string, string>("Remarks", b2bitem.Remarks),
                new KeyValuePair<string, string>("QueueTimeOutURL", b2bitem.QueueTimeOutURL),
                new KeyValuePair<string, string>("ResultURL", b2bitem.ResultURL)
            };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, BaseAddress)
            {
                Content = new FormUrlEncodedContent(values)
            };

            request.Headers.Authorization = new AuthenticationHeaderValue("Authorization", "Bearer " + token);

            HttpResponseMessage response = await _httpclient.SendAsync(request);      

            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
