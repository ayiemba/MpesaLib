## LipaNaMpesaOnline/MpesaExpress (STK Push) Payment Request

```c#

var MpesaExpressObject = new LipaNaMpesaOnlineDto
{
	AccountReference = "",
	Amount = "", //Money being Transacted (Obey M-PESA Limits)
	PartyA = "",
	PartyB = "174379",
	BusinessShortCode = "174379",
	CallBackURL = "https://yourappsdomain.com/api/callback", // create your own callback url
	Passkey = "",	//get passkey from daraja			  
	PhoneNumber = "", 				
	TransactionDesc = "test"
	//Note that you don't have to provide Password and Timestamp, these are calculated for you automatically provided you enter passkey.
};

//Make payment request 
var paymentrequest = await _mpesaClient.MakeLipaNaMpesaOnlinePaymentAsync(MpesaExpressObject, accesstoken, RequestEndPoint.LipaNaMpesaOnline));

```