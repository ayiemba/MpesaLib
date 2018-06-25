using MpesaLib;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace MpesaLibTests
{
    partial class Program
    {
        public static void Main(string[] args)
        {
            //var consumerKey = "HzROja3XIZJiCIfzsMj59xyL2GR2S52C";
            //var consumerSecret = "c7cB7AU3c0uyYxxd";

            var items = new Items();

            var transaction = new MpesaTransaction();



            

           var lipaNaMpesa = transaction.LipaNaMpesaOnline(items.lipaOnline);           

            var b2c = transaction.BusinessToCustomer(items.b2c);


            Console.WriteLine("Starting Simulations...");

            Console.WriteLine(lipaNaMpesa.Result);
            Console.WriteLine("=================================================");
            Console.WriteLine(b2c.Result);

        }

       
    }
}
