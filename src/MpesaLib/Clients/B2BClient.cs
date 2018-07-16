﻿using MpesaLib.Interfaces;
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
            
            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var values = new Dictionary<string, string>
            {
                { "Initiator", b2bitem.Initiator },
                { "SecurityCredential", b2bitem.SecurityCredential },
                {"CommandID", b2bitem.CommandID },
                {"SenderIdentifierType", b2bitem.SenderIdentifierType },
                {"RecieverIdentifierType", b2bitem.RecieverIdentifierType },
                {"Amount", b2bitem.Amount },
                {"PartyA", b2bitem.PartyA },
                {"PartyB", b2bitem.PartyB },
                {"AccountReference", b2bitem.AccountReference },
                {"Remarks", b2bitem.Remarks },
                { "QueueTimeOutURL", b2bitem.QueueTimeOutURL},
                { "ResultURL", b2bitem.ResultURL }
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
