using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MpesaLib.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class LipaNaMpesaOnline
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("BusinessShortCode")]
        public string BusinessShortCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("Timestamp")]
        public string Timestamp { get; set; } = DateTime.Now.ToString("yyyyMMddHHmmss");

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("TransactionType")]
        public string TransactionType { get; set; } = "CustomerPayBillOnline";

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("Amount")]
        public string Amount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("PartyA")]
        public string PartyA { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("PartyB")]
        public string PartyB { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("PhoneNumber")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("CallBackURL")]
        public string CallBackURL { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("AccountReference")]
        public string AccountReference { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("TransactionDesc")]
        public string TransactionDesc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Passkey { get; set; }

        private string CalculatePassword => Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(PartyB + Passkey + Timestamp));

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("Password")]
        public string Password { get => CalculatePassword; set => value = CalculatePassword; } 

        
        
    }

}
