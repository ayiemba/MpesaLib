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
    public class TransactionStatusClient : ITransactionStatusClient
    {
        private readonly HttpClient _httpclient;
        public TransactionStatusClient(HttpClient httpClient)
        {
            _httpclient = httpClient;
        }

        public Uri BaseAddress { get; set; } = new Uri("https://sandbox.safaricom.co.ke/mpesa/transactionstatus/v1/query");

        public async Task<string> GetTransactionStatus(MpesaTransactionStatus transactionStatus, string accesstoken)
        {           
            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);

            var values = new Dictionary<string, string>
            {
                {"Initiator", transactionStatus.Initiator },
                {"SecurityCredential", transactionStatus.SecurityCredential },
                {"CommandID", transactionStatus.CommandID },
                {"TransactionID", transactionStatus.TransactionID },
                {"PartyA", transactionStatus.PartyA },
                {"IdentifierType", transactionStatus.IdentifierType },
                {"ResultURL", transactionStatus.ResultURL },
                {"QueueTimeOutURL", transactionStatus.QueueTimeOutURL },
                {"Remarks", transactionStatus.Remarks },
                { "Occasion", transactionStatus.Occasion }

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
