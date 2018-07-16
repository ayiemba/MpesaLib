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
    public class TransactionReversalClient : ITransactionReversalClient
    {
        private readonly HttpClient _httpclient;
        public TransactionReversalClient(HttpClient httpClient)
        {
            _httpclient = httpClient;
        }
        public async Task<string> ReverseTransaction(Reversal reversal, string accesstoken)
        {
            var BaseAddress = new Uri("https://sandbox.safaricom.co.ke/mpesa/reversal/v1/request");

            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);            

            var values = new Dictionary<string, string>
            {
                {"Initiator", reversal.Initiator },
                {"SecurityCredential", reversal.SecurityCredential },
                {"CommandID", reversal.CommandID },
                {"TransactionID", reversal.TransactionID },
                {"Amount", reversal.Amount },
                {"ReceiverParty", reversal.ReceiverParty },
                {"RecieverIdentifierType", reversal.RecieverIdentifierType },
                {"ResultURL", reversal.ResultURL },
                {"QueueTimeOutURL", reversal.QueueTimeOutURL },
                {"Remarks", reversal.Remarks },
                { "Occasion", reversal.Occasion }

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
