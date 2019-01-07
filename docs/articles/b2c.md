
## B2C Payment Request
```c#
//B2C Object
var BusinessToCustomerObject = new BusinessToCustomer
{
	Remarks = "test",
	Amount = "1",
	CommandID = "BusinessPayment",
	InitiatorName = "safaricom.15",
	Occasion = "test",
	PartyA = "603047",
	PartyB = "254708374149",
	QueueTimeOutURL = "https://blablabala/timeoutendpoint",
	ResultURL = "https://blablabala/resultendpoint",
	SecurityCredential = B2CsecurityCred //see #12 below on how to get security credential
};
var b2crequest = await _mpesaClient.MakeB2CPaymentAsync(BusinessToCustomerObject, accesstoken, "mpesa/b2c/v1/paymentrequest");
```