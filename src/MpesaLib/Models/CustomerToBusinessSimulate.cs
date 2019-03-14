using System;
using System.Collections.Generic;
using System.Text;

namespace MpesaLib.Models
{
    public class CustomerToBusinessSimulate
    {
        public string ShortCode { get; set; }
        public string CommandID { get; set; } = "CustomerPayBillOnline";
        public string Amount { get; set; }
        public string Msisdn { get; set; }
        public string BillRefNumber { get; set; }
    }
}
