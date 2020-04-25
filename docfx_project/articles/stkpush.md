## LipaNaMpesaOnline/MpesaExpress (STK Push) Payment Request

```c#
// initialize object with data
var MpesaExpressObject = new LipaNaMpesaOnlineDto
(
	"BusinessShortCode",// businessShortCode
    	Timestamp, //Timestamp
        "TransactionType",  //transactionType
        "Amount", // amount
        "PartyA" ,// partyA
        "PartyB" ,// partyB
        "PhoneNumber", // phoneNumber
        "CallBackURL", // callBackUrl
        "AccountReference" ,//accountReference
        "TransactionDesc" ,//transactionDescription
        "Passkey" //passkey
);

//Make payment request 
var paymentrequest = await _mpesaClient.MakeLipaNaMpesaOnlinePaymentAsync(MpesaExpressObject, accesstoken, RequestEndPoint.LipaNaMpesaOnline));

```
