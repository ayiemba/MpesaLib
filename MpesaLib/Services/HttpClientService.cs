using MpesaLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MpesaLib.Services
{
    public class HttpClientService : IHttpClientService
    {
        private static HttpClient client = new HttpClient();

        public HttpClient Client => client;
    }
}
