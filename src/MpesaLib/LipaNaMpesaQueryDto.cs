using System;
using System.Collections.Generic;
using System.Text;

namespace MpesaLib
{
    public class LipaNaMpesaQueryDto
    {
        public string BusinessShortCode { get; set; }
        public string Password { get; set; }
        public string Timestamp { get; set; }
        public string CheckoutRequestID { get; set; }
    }
}
