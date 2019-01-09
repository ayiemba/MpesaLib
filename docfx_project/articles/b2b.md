
## B2B Payment Request

```c#
var BusinessToBusinessObject = new BusinessToBusinessDto
{
	AccountReference = "test",
	Initiator = "safaricom.13",
	Amount = "1",
	PartyA = "603047",
	PartyB = "600000",
	CommandID = "MerchantToMerchantTransfer",// Please chack the correct command from Daraja
	QueueTimeOutURL = "https://blablabla/callback",
	RecieverIdentifierType = "4", //Read on identifier types from daraja
	SecurityCredential = "", // Use MpesaLib.Helpers.Credentisla class to generate security credential
	SenderIdentifierType = "4",
	ResultURL = "https://blablabla/callback",
	Remarks = "payment"
};

var b2brequest = await _mpesaClient.MakeB2BPaymentAsync(BusinessToBusinessObject, accesstoken, "mpesa/b2b/v1/paymentrequest");
```
