using MpesaLib.Models;
using System;

namespace WebApp.Models
{
        public class Items
        {
            public BusinessToCustomer b2c = new BusinessToCustomer
            {
                Remarks = "test",
                Amount = "10",
                CommandID = "BusinessPayment",
                InitiatorName = "testapi",
                Occasion = "test",
                PartyA = "600157",
                PartyB = "254708374149",
                QueueTimeOutURL = "http://mockbin.org/bin/7e613f32-e00a-48d3-86e4-9088e4f96ffa",
                ResultURL = "http://mockbin.org/bin/7e613f32-e00a-48d3-86e4-9088e4f96ffa",
                SecurityCredential = "BPWkcbLlh3gLaAe5rxIdHC7zKpxHZJmuYAnIsTsXUdH1O5tcOQ/HCtg0GVs87GUfDkQcazyztAViU/kCQjua199XwCoYakNnZwiNC8UgF4t4PzX2g0PogVd4Pn5UQpMA1+B8DeVszF10KN2w6KAaMugSNbJqb9b6iE2ykDRycANmN6hbtfAEA7el5y4fas6+KImiBmhmplRc8P9GvohLYxRLPct3q0A58Sf9mz9w5uuKF7ZmYJlpI8YY2pWQq1GIedYY63OIFamHu1PVufZmbc/lmq/hWO9C3AcfKN9dnP6bkO4Y/TeC8HvvkooxpWl+eCe7AyUAk9OIjcIiAzh05w=="
            };


            public LipaNaMpesaOnline lipaOnline = new LipaNaMpesaOnline
            {
                AccountReference = "test",
                Amount = "10",
                PartyA = "254708374149",
                PartyB = "174379",
                BusinessShortCode = "174379",
                CallBackURL = "http://mockbin.org/bin/7e613f32-e00a-48d3-86e4-9088e4f96ffa",
                Password = "M0bileST!!",
                PhoneNumber = "254708374149",
                Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(),
                TransactionDesc = "test"

            };

            public CustomerToBusinessSimulate c2b = new CustomerToBusinessSimulate
            {
                ShortCode = "600157",
                Amount = "10",
                BillRefNumber = "",
                Msisdn = "254708374149"
            };
        }

       
    }

