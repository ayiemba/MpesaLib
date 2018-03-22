
namespace MpesaLib
{
    public class LipaNaMpesaOnline
    {       
        public string Amount { get; set; } = "10";
        public string PartyA { get; set; } = "254708374149";
        public string PartyB { get; set; } = "174379";
        public string TransactionDesc { get; set; } = "test";
        public string AccountReference { get; set; } = "test";
        public string CallBackURL { get; set; } = "https://hookbin.com/bin/Z8aaN0Ob";
        public string PhoneNumber { get; set; } = "254708374149";
        public string TransactionType { get; internal set; } = "CustomerPayBillOnline";
        public string Timestamp { get; set; } = "";
        public string Password { get; set; } = "bfb279f9aa9bdbcf158e97dd71a467cd2e0c893059b10f78e6b72ada1ed2c919";
        public string BusinessShortCode { get;set; } = "174379";
    }
}
