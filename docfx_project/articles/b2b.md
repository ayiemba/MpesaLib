
## B2B Payment Request

```c#
var BusinessToBusinessObject = new BusinessToBusinessDto
{
	AccountReference = "test",
	Initiator = "safaricom.13",
	Amount = "1",
	PartyA = "603047",
	PartyB = "600000",
	CommandID = "BusinessPayBill",// Please use the correct command from Daraja -usage depends on what is enabled for your shortcode
	QueueTimeOutURL = "https://blablabla/callback",
	RecieverIdentifierType = "4", //Read on reciver identifier types from daraja
	SecurityCredential = "", // Use MpesaLib.Helpers.Credentials class to generate security credential
	SenderIdentifierType = "4",
	ResultURL = "https://blablabla/callback",
	Remarks = "payment"
};

var b2brequest = await _mpesaClient.MakeB2BPaymentAsync(BusinessToBusinessObject, accesstoken, RequestEndPoint.BusinessToBusiness);
```
