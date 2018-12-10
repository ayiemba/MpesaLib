using System;
using System.Collections.Generic;
using System.Text;

namespace MpesaLib
{
    public class MpesaTransactionStatusDto
    {
        public string Initiator { get; set; }
        public string SecurityCredential { get; set; }
        public string CommandID { get; } = "TransactionStatusQuery";
        public string TransactionID { get; set; }
        public string PartyA { get; set; }
        public string IdentifierType { get; set; }
        public string Remarks { get; set; }
        public string QueueTimeOutURL { get; set; }
        public string ResultURL { get; set; }
        public string Occasion { get; set; }
    }
}
