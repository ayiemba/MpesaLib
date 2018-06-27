using MpesaLib;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MpesaLibTests
{
    partial class Program
    {
        public static void Main(string[] args)
        {
            //These Keys should come from a configuration file
            //DO NOT rely on these keys, they are just for testing and are deleted once used
            //Create your own key from https://developer.safaricom.co.ke/
            var consumerKey = "vHlWQgAamTdrA2MFRUGdfVCKESOvBGmu";
            var consumerSecret = "lG7aQLJOdXmVwVAg";

            var items = new Items();

            var transaction = new MpesaTransaction(new HttpClient(), consumerKey, consumerSecret);

            var lipaNaMpesa = transaction.LipaNaMpesaOnline(items.lipaOnline);           

            var b2c = transaction.BusinessToCustomer(items.b2c);

            var c2bsim = transaction.CustomerToBusinessSimulate(items.c2b);


            Console.WriteLine("Starting Simulations...");

            //Console.WriteLine(lipaNaMpesa.Result);
            Console.WriteLine("=================================================");
            //Console.WriteLine(b2c.Result);
            Console.WriteLine("===============================================");
            Console.WriteLine(c2bsim.Result);

        }

       
    }
}
