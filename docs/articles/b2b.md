
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
	QueueTimeOutURL = "https://blablabala/callback",
	RecieverIdentifierType = "4", //Read on identifier types from daraja
	SecurityCredential = B2BsecurityCred, // See #12 on how to generate security credential
	SenderIdentifierType = "4",
	ResultURL = "https://blablabala/callback",
	Remarks = "payment"
};
var b2brequest = await _mpesaClient.MakeB2BPaymentAsync(BusinessToBusinessObject, accesstoken, "mpesa/b2b/v1/paymentrequest");
```