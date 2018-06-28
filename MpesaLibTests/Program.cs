using Microsoft.Extensions.DependencyInjection;
using MpesaLib.Interfaces;
using MpesaLib.Services;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace MpesaLibTests
{
    partial class Program
    {
        public static void Main(string[] args)
        {
            var httpClientService = new HttpClientService();

            var transaction = new Transactions(httpClientService);

            transaction.MakeTransaction();

        }

       
    }
}
