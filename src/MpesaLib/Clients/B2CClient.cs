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
    /// <summary>
    /// Instantiate this claa to make a Business to Customer Mpesa payment.
    /// </summary>
    public class B2CClient : IB2CClient
    {
        private readonly HttpClient _httpclient;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpClient"></param>
        public B2CClient(HttpClient httpClient)
        {
            _httpclient = httpClient;
        }

        /// <summary>
        /// Defaults to sandbox URL. Override with Mpesa production url when deploying to a production environment.
        /// </summary>
        public string BaseUri { get; set; } = "https://sandbox.safaricom.co.ke/mpesa/b2c/v1/paymentrequest";

        /// <summary>
        /// Make Business to Customer Mpesa Payment
        /// </summary>
        /// <param name="b2citem">B2C payment request object</param>
        /// <param name="accesstoken">Access token generate by AuthClient</param>
        /// <returns>Returns a JSON string that you should deserialize</returns>
        public async Task<string> PayCustomer(BusinessToCustomer b2citem, string accesstoken)
        {

            Uri BaseAddress = new Uri(BaseUri);
            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);

            var values = new Dictionary<string, string>
            {
                {"InitiatorName", b2citem.InitiatorName },
                {"SecurityCredential", b2citem.SecurityCredential },
                {"CommandID", b2citem.CommandID },
                {"Amount", b2citem.Amount },
                {"PartyA", b2citem.PartyA },
                {"PartyB", b2citem.PartyB },
                {"Remarks", b2citem.Remarks },
                {"QueueTimeOutURL", b2citem.QueueTimeOutURL },
                {"ResultURL", b2citem.ResultURL },
                { "Occasion", b2citem.Occasion }
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
