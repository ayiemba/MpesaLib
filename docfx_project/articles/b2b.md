
## B2B Payment Request

```c#
var BusinessToBusinessObject = new BusinessToBusinessDto
(
	"test", //AccountReference 
	"safaricom.13", //Initiator 
	"1500", //Amount 
	"603047", //PartyA 
	"600000", //PartyB
	"BusinessPayBill",// Please use the correct command -usage depends on what is enabled for your shortcode
	"https://blablabla/callback", //QueueTimeOutURL 
	"4", // RecieverIdentifierType  - Read on receiver identifier types from daraja
	"security credential", //SecurityCredential - Use MpesaLib.Helpers.Credentials class to generate security credential
	"4", //SenderIdentifierType
	"https://blablabla/callback", //ResultURL
	"payment" //Remarks
);

var b2brequest = await _mpesaClient.MakeB2BPaymentAsync(BusinessToBusinessObject, accesstoken, RequestEndPoint.BusinessToBusiness);

```
