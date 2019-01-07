

## C2B Register Urls Request
```c#
var RegisterC2BUrlObject = new CustomerToBusinessRegisterUrlDto
{
	ConfirmationURL = "https://blablabala/api/confirm",
	ValidationURL = "https://blablabala/api/validate",
	ResponseType = "Cancelled",
	ShortCode = "603047"
};
var c2bRegisterUrlrequest = await _mpesaClient.RegisterC2BUrlAsync(RegisterC2BUrlObject, accesstoken, "mpesa/c2b/v1/registerurl");
```

## C2B Payment Request
```c#
//C2B Object
Var CustomerToBusinessSimulateObject = new CustomerToBusinessSimulateDto
{
	ShortCode = "603047",
	Amount = "10",
	BillRefNumber = "account",
	Msisdn = "254708374149",
	CommandID = "CustomerPayBillOnline"
};

var c2brequest = await _mpesaClient.MakeC2BPaymentAsync(CustomerToBusinessSimulateObject, accesstoken, "mpesa/c2b/v1/simulate");
```