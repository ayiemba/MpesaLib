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

            _httpclient.DefaultRequestHeaders.Accept.Clear();
            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //_httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", "Bearer " + accesstoken);

            var values = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Initiator", reversal.Initiator),
                new KeyValuePair<string, string>("SecurityCredential", reversal.SecurityCredential),
                new KeyValuePair<string, string>("CommandID", reversal.CommandID),
                new KeyValuePair<string, string>("TransactionID", reversal.TransactionID),
                new KeyValuePair<string, string>("Amount", reversal.Amount),
                new KeyValuePair<string, string>("ReceiverParty", reversal.ReceiverParty),
                new KeyValuePair<string, string>("RecieverIdentifierType", reversal.RecieverIdentifierType),
                new KeyValuePair<string, string>("ResultURL", reversal.ResultURL),
                new KeyValuePair<string, string>("QueueTimeOutURL", reversal.QueueTimeOutURL),
                new KeyValuePair<string, string>("Remarks", reversal.Remarks),
                new KeyValuePair<string, string>("Occasion", reversal.Occasion)

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
