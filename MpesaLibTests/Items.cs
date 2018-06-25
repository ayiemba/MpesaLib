using MpesaLib.Models;
using System;

namespace MpesaLibTests
{
    partial class Program
    {
        public class Items
        {
            public BusinessToCustomer b2c = new BusinessToCustomer
            {
                Remarks= "test",
                Amount = "10", 
                CommandID = "BusinessPayment",
                InitiatorName = "testapi",
                Occasion = "test",
                PartyA = "600525",
                PartyB = "254708374149",
                QueueTimeOutURL = "https://hookbin.com/bin/Z8aaN0Ob",
                ResultURL = "https://hookbin.com/bin/Z8aaN0Ob",
                SecurityCredential = "FE1MVFVDYtgrsJMfWYd8w4pxeup1fhR/qjgcacwA1JiWgn1eSXCc28+N00fm213AWr7yg4ltL+jFJG9szVcuQ2wtPywrH80a0lU9WqycBMWL7C6G6oRrD/mAeeFxfDnLY3yx5D9Anp+GlC3LH+srThvPbNoU4ZJiMwKq4IDedjVPza8rf1rjW4in4zxbbX2Z3++xUWVqix6rCwLCNNHgV7OzrLljCRQdI+hzCfjsppHc8gy5hxHjW3QoaBCBxGHwlhs/jJxh/dv37JxK5y85rfbqd/Vv51RIfvfsxurY/bH7OoKQ06XFCHl6cC27rPMaRsDp9GM7WJkXLYt4SE6dtg=="


            };


            public LipaNaMpesaOnline lipaOnline = new LipaNaMpesaOnline
            {
                AccountReference = "test",
                Amount = "10",
                PartyA = "254708374149",
                PartyB = "174379",
                BusinessShortCode = "174379",
                CallBackURL = "https://hookbin.com/bin/Z8aaN0Ob",
                Password = "bfb279f9aa9bdbcf158e97dd71a467cd2e0c893059b10f78e6b72ada1ed2c919",
                PhoneNumber = "254708374149",
                Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(),
                TransactionDesc = "test",
                TransactionType = "CustomerPayBillOnline"
            };
        }

       
    }
}
