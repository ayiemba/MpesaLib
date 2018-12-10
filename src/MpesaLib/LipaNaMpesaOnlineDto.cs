using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MpesaLib
{

    public class LipaNaMpesaOnlineDto
    {
       
        [JsonProperty("BusinessShortCode")]
        public string BusinessShortCode { get; set; }

        [JsonProperty("Timestamp")]
        public string Timestamp { get; set; } = DateTime.Now.ToString("yyyyMMddHHmmss");

        [JsonProperty("TransactionType")]
        public string TransactionType { get; set; } = TransactType.CustomerBuyGoodsOnline;

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

        [JsonProperty("TransactionDesc")]
        public string TransactionDesc { get; set; }
        
        public string Passkey { get; set; }

        private string CalculatePassword => Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(PartyB + Passkey + Timestamp));
        
        [JsonProperty("Password")]
        public string Password { get => CalculatePassword; set => value = CalculatePassword; } 

        
        
    }

}
