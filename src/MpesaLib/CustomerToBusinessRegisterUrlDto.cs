using System;
using System.Collections.Generic;
using System.Text;

namespace MpesaLib
{
    public class CustomerToBusinessRegisterUrlDto
    {
        public string ShortCode { get; set; }
        public string ResponseType { get; set; }
        public string ConfirmationURL { get; set; }
        public string ValidationURL { get; set; }
    }
}
