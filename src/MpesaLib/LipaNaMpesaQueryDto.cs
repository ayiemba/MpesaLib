using Newtonsoft.Json;
using System;
using System.Text;

namespace MpesaLib
{
    /// <summary>
    /// LipaNaMpesa Query transaction status data transfer object
    /// </summary>
    public class LipaNaMpesaQueryDto
    {
        /// <summary>
        /// This is organizations shortcode (Paybill or Buygoods - A 5 to 6 digit account number) 
        /// used to identify an organization and receive the transaction.
        /// </summary>
        [JsonProperty("BusinessShortCode")]
        public string BusinessShortCode { get; set; }

        /// <summary>
        /// Lipa Na Mpesa Online PassKey
        /// Provide the Passkey only if you want MpesaLib to Encode the Password for you.
        /// </summary>
        public string Passkey { get; set; }

        /// <summary>
        /// This is the password used for encrypting the request sent: A base64 encoded string. 
        /// The base64 string is a combination of Shortcode+Passkey+Timestamp
        /// The Defualt value is set by a private method that creates the necessary base64 encoded string
        /// Don't set this property if you have set the passKey property.
        /// </summary>
        [JsonProperty("Password")]
        public string Password { get => CalculatePassword; set => value = CalculatePassword; }
        

        /// <summary>
        /// This is the Timestamp of the transaction, 
        /// normaly in the formart of YEAR+MONTH+DATE+HOUR+MINUTE+SECOND (YYYYMMDDHHMMSS) 
        /// Each part should be atleast two digits apart from the year which takes four digits.
        /// By Default this property is set to <c>DateTime.Now.ToString("yyyyMMddHHmmss")</c> so you don't have to set its value.
        /// </summary>
        [JsonProperty("Timestamp")]
        public string Timestamp { get; set; } = DateTime.Now.ToString("yyyyMMddHHmmss");

        /// <summary>
        /// This is a global unique identifier of the processed checkout transaction request.
        /// e.g ws_CO_DMZ_123212312_2342347678234
        /// </summary>
        [JsonProperty("CheckoutRequestID")]
        public string CheckoutRequestID { get; set; }
      

        /// <summary>
        /// This method creates the necessary base64 encoded string that encrypts the request sent 
        /// </summary>
        private string CalculatePassword => Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(BusinessShortCode + Passkey + Timestamp));

        
    }
}
