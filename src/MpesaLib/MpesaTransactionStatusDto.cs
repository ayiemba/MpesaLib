using Newtonsoft.Json;

namespace MpesaLib
{
    /// <summary>
    /// Mpesa Transaction Status Query data transfer object
    /// </summary>
    public class MpesaTransactionStatusDto
    {
        /// <summary>
        /// The name of Initiator to initiating  the request.
        /// This is the credential/username used to authenticate the transaction request.
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
        /// Takes only 'TransactionStatusQuery' command id
        /// The default value has been set to that so you don't have to set this property.
        /// </summary>
        [JsonProperty("CommandID")]
        public string CommandID { get; set; } = "TransactionStatusQuery";

        /// <summary>
        /// Unique identifier to identify a transaction on M-Pesa. e.g LKXXXX1234
        /// </summary>
        [JsonProperty("TransactionID")]
        public string TransactionID { get; set; }

        /// <summary>
        /// Organization/MSISDN receiving the transaction
        /// </summary>
        [JsonProperty("PartyA")]
        public string PartyA { get; set; }

        /// <summary>
        /// Type of organization receiving the transaction
        /// 1 – MSISDN
        /// 2 – Till Number
        /// 4 – Organization short code
        /// </summary>
        [JsonProperty("IdentifierType")]
        public string IdentifierType { get; set; }

        /// <summary>
        /// Comments that are sent along with the transaction
        /// </summary>
        [JsonProperty("Remarks")]
        public string Remarks { get; set; }

        /// <summary>
        /// The path that stores information of time out transaction. https://ip or domain:port/path
        /// </summary>
        [JsonProperty("QueueTimeOutURL")]
        public string QueueTimeOutURL { get; set; }

        /// <summary>
        /// The path that stores information of transaction. https://ip or domain:port/path
        /// </summary>
        [JsonProperty("ResultURL")]
        public string ResultURL { get; set; }

        /// <summary>
        /// Optional Parameter. (upto 100 characters)
        /// </summary>
        [JsonProperty("Occasion")]
        public string Occasion { get; set; }
    }
}
