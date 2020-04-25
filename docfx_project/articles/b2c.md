
## B2C Payment Request
```c#
//B2C Object
var BusinessToCustomerObject = new BusinessToCustomerDto
(
	"test",//Remarks
	"1", //Amount
	"BusinessPayment", //CommandID
	"safaricom.15", //InitiatorName
	"test", //Occasion
	"603047", //PartyA
	"254708374149", //PartyB
	"https://blablabla/timeoutendpoint", //QueueTimeOutURL
	"https://blablabla/resultendpoint", //ResultURL
	"security credential" //SecurityCredential
);

var b2crequest = await _mpesaClient.MakeB2CPaymentAsync(BusinessToCustomerObject, accesstoken, RequestEndPoint.BusinessToCustomer);

```
