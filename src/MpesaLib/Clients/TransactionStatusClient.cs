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
    public class TransactionStatusClient : ITransactionStatusClient
    {
        private readonly HttpClient _httpclient;
        public TransactionStatusClient(HttpClient httpClient)
        {
            _httpclient = httpClient;
        }
        public async Task<string> GetData(MpesaTransactionStatus transactionStatus, string accesstoken)
        {
            var BaseAddress = new Uri("https://sandbox.safaricom.co.ke/mpesa/transactionstatus/v1/query");

            _httpclient.DefaultRequestHeaders.Accept.Clear();
            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //_httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", "Bearer " + accesstoken);

            var values = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Initiator", transactionStatus.Initiator),
                new KeyValuePair<string, string>("SecurityCredential", transactionStatus.SecurityCredential),
                new KeyValuePair<string, string>("CommandID", transactionStatus.CommandID),
                new KeyValuePair<string, string>("TransactionID", transactionStatus.TransactionID),
                new KeyValuePair<string, string>("PartyA", transactionStatus.PartyA),
                new KeyValuePair<string, string>("IdentifierType", transactionStatus.IdentifierType),
                new KeyValuePair<string, string>("ResultURL", transactionStatus.ResultURL),
                new KeyValuePair<string, string>("QueueTimeOutURL", transactionStatus.QueueTimeOutURL),
                new KeyValuePair<string, string>("Remarks", transactionStatus.Remarks),
                new KeyValuePair<string, string>("Occasion", transactionStatus.Occasion)

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
