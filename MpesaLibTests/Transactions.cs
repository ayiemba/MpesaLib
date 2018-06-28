using MpesaLib;
using MpesaLib.Services;
using System;
using static MpesaLibTests.Program;

namespace MpesaLibTests
{
    public class Transactions
    {
        private readonly HttpClientService _transactionService;

        public Transactions(HttpClientService transactionService)
        {
            _transactionService = transactionService;

        }

        public void MakeTransaction()
        {

            //These Keys should come from a configuration file
            //DO NOT rely on these keys, they are just for testing and are deleted once used
            //Create your own key from https://developer.safaricom.co.ke/
            var consumerKey = "vHlWQgAamTdrA2MFRUGdfVCKESOvBGmu";
            var consumerSecret = "lG7aQLJOdXmVwVAg";

            var items = new Items();

            var transaction = new MpesaTransaction(_transactionService, consumerKey, consumerSecret);

            var lipaNaMpesa = transaction.LipaNaMpesaOnline(items.lipaOnline);
            Console.WriteLine(lipaNaMpesa.Result);
            Console.WriteLine("=================================================");

            var b2c = transaction.BusinessToCustomer(items.b2c);
            Console.WriteLine(b2c.Result);
            Console.WriteLine("===============================================");

            var c2bsim = transaction.CustomerToBusinessSimulate(items.c2b);
            Console.WriteLine(c2bsim.Result);





        }

        
    }
}
