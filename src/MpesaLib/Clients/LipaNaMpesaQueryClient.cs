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
    /// <summary>
    /// Instantiate this class to make a Lipa na Mpesa online Query api call
    /// </summary>
    public class LipaNaMpesaQueryClient : ILipaNaMpesaQueryClient
    {
        private readonly HttpClient _httpclient;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpClient"></param>
        public LipaNaMpesaQueryClient(HttpClient httpClient)
        {
            _httpclient = httpClient;
        }

        /// <summary>
        /// Defaults to sandbox URL. Override with Mpesa production url when deploying to a production environment.
        /// </summary>
        public string BaseUri { get; set; } = "https://sandbox.safaricom.co.ke/mpesa/stkpushquery/v1/query";

        /// <summary>
        /// Lipa Na Mpesa Online Query
        /// </summary>
        /// <param name="mpesaQuery">Lipa Na Mpesa Online Query Object</param>
        /// <param name="accesstoken">Access token Generated by AuthClient</param>
        /// <returns>Returns a JSON string that you should deserialize</returns>
        public async Task<string> MakeMpesaQuery(LipaNaMpesaQuery mpesaQuery, string accesstoken)
        {
            Uri BaseAddress = new Uri(BaseUri);

            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);

            var values = new Dictionary<string, string>
            {
                {"BusinessShortCode", mpesaQuery.BusinessShortCode },
                {"Password", mpesaQuery.Password },
                {"Timestamp", mpesaQuery.Timestamp },
                { "CheckoutRequestID", mpesaQuery.CheckoutRequestID }

            };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, BaseAddress)
            {
                Content = new StringContent(JsonConvert.SerializeObject(values).ToString(), Encoding.UTF8, "application/json")
            };

            HttpResponseMessage response;
            try
            {
                response = await _httpclient.SendAsync(request);
            }
            catch(Exception e)
            {
                throw new ApplicationException("Something went wrong:", e);
            }
           
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
