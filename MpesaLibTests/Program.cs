using MpesaLib;
using System;
using System.Threading.Tasks;

namespace MpesaLibTests
{
    partial class Program
    {
        public static void Main(string[] args)
        {
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
