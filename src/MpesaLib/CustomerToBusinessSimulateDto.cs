using Newtonsoft.Json;

namespace MpesaLib
{
    /// <summary>
    /// C2B Simulate data transfer object
    /// </summary>
    public class CustomerToBusinessSimulateDto
    {

        /// <summary>
        /// This is the Short Code receiving the amount being transacted.
        /// </summary>
        [JsonProperty("ShortCode")]
        public string ShortCode { get; private set; }

        /// <summary>
        /// This is a unique identifier of the transaction type: There are two types of these Identifiers:
        /// CustomerPayBillOnline: This is used for Pay Bills shortcodes.
        /// CustomerBuyGoodsOnline: This is used for Buy Goods shortcodes.
        /// Buy Default this property is set to CustomerPayBillOnline
        /// </summary>
        [JsonProperty("CommandID")]
        public string CommandID { get; private set; } = TransactType.CustomerPayBillOnline;

        /// <summary>
        /// This is the amount being transacted. The parameter expected is a numeric value.
        /// </summary>
        [JsonProperty("Amount")]
        public string Amount { get; private set; }

        /// <summary>
        /// This is the phone number initiating the C2B transaction.(format: 2547XXXXXXXX)
        /// </summary>
        [JsonProperty("Msisdn")]
        public string Msisdn { get; private set; }

        /// <summary>
        /// This is used on CustomerPayBillOnline option only. 
        /// This is where a customer is expected to enter a unique bill identifier, e.g an Account Number. 
        /// </summary>
        [JsonProperty("BillRefNumber")]
        public string BillRefNumber { get; private set; }

        public CustomerToBusinessSimulateDto(string shortCode, string commandId, string amount, string msisdn, 
            string billReferenceNumber)
        {
            ShortCode = shortCode;
            CommandID = commandId;
            Amount = amount;
            Msisdn = msisdn;
            BillRefNumber = billReferenceNumber;
        }
    }
}
