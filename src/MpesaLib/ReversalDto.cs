using Newtonsoft.Json;

namespace MpesaLib
{
    /// <summary>
    /// Transaction reversal data transfer object
    /// </summary>
    public class ReversalDto
    {
        /// <summary>
        /// The name of Initiator to initiating  the request
        /// </summary>
        [JsonProperty("Initiator")]
        public string Initiator { get; set; }

        /// <summary>
        /// Encrypted password for the initiator to authenticate the transaction request.
        /// Use <c>Credentials.EncryptPassword</c> method available under MpesaLib.Helpers to encrypt the password.
        /// </summary>
        [JsonProperty("SecurityCredential")]
        public string SecurityCredential { get; set; }

        /// <summary>
        /// Takes only 'TransactionReversal' Command id.
        /// The default value has been set to that so you don't have to set this property.
        /// </summary>
        [JsonProperty("CommandID")]
        public string CommandID { get; set; } = "TransactionReversal";

        /// <summary>
        /// Unique identifier to identify a transaction on M-Pesa. e.g LKXXXX1234
        /// </summary>
        [JsonProperty("TransactionID")]
        public string TransactionID { get; set; }

        /// <summary>
        /// Organization receiving the transaction (Shortcode)
        /// </summary>
        [JsonProperty("ReceiverParty")]
        public string ReceiverParty { get; set; }

        /// <summary>
        /// Type of organization receiving the transaction.
        /// 11 - Organization Identifier on M-Pesa
        /// </summary>
        [JsonProperty("RecieverIdentifierType")]
        public string RecieverIdentifierType { get; set; }

        /// <summary>
        /// Comments that are sent along with the transaction. (Upto 100 characters)
        /// </summary>
        [JsonProperty("Remarks")]
        public string Remarks { get; set; }

        /// <summary>
        /// The path that stores information of time out transaction.
        /// </summary>
        [JsonProperty("QueueTimeOutURL")]
        public string QueueTimeOutURL { get; set; }

        /// <summary>
        /// The path that stores information of transaction 
        /// </summary>
        [JsonProperty("ResultURL")]
        public string ResultURL { get; set; }

        /// <summary>
        /// Optional Parameter (upto 100 characters)
        /// </summary>
        [JsonProperty("Occasion")]
        public string Occasion { get; set; }
    }
}
