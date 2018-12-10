using System;
using System.Collections.Generic;
using System.Text;

namespace MpesaLib
{
    public class BusinessToCustomerDto
    {
        public string InitiatorName { get; set; }
        public string SecurityCredential { get; set; }
        public string CommandID { get; set; }
        public string Amount { get; set; }
        public string PartyA { get; set; }
        public string PartyB { get; set; }
        public string Remarks { get; set; }
        public string QueueTimeOutURL { get; set; }
        public string ResultURL { get; set; }
        public string Occasion { get; set; }
    }
}
