using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MpesaLib.Models
{
    public class LipaNaMpesaOnline
    {
        [JsonProperty("BusinessShortCode")]
        public string BusinessShortCode { get; set; }

        [JsonProperty("Timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("TransactionType")]
        public string TransactionType { get; set; } = "CustomerPayBillOnline";

        [JsonProperty("Amount")]
        public string Amount { get; set; }

        [JsonProperty("PartyA")]
        public string PartyA { get; set; }

        [JsonProperty("PartyB")]
        public string PartyB { get; set; }

        [JsonProperty("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("CallBackURL")]
        public string CallBackURL { get; set; }

        [JsonProperty("AccountReference")]
        public string AccountReference { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; } 

        [JsonProperty("TransactionDesc")]
        public string TransactionDesc { get; set; }
        
    }

}
