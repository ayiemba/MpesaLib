using System;
using System.Collections.Generic;
using System.Text;

namespace MpesaLib.Models
{
    public class AccountBalance
    {
        public string Initiator { get; set; }
        public string SecurityCredential { get; set; }
        public string CommandID { get; set; } = "AccountBalance";
        public string Amount { get; set; }
        public string PartyA { get; set; }
        public string IdentifierType { get; set; }
        public string Remarks { get; set; }
        public string QueueTimeOutURL { get; set; }
        public string ResultURL { get; set; }
    }
}
