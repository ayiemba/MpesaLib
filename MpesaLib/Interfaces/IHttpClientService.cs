using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MpesaLib.Interfaces
{
    public interface IHttpClientService
    {
        HttpClient Client { get; }
    }
}
