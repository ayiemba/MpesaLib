

## LipaNaMpesaOnline/MpesaExpress Transaction Query Request
```c#
var QueryLipaNaMpesaTransactionObject = new LipaNaMpesaQueryDto
{
	BusinessShortCode = "174379",
	CheckoutRequestID = "",
	Password = "", //this will change in future to use passkey
	Timestamp = "" //this will be taken care of with future release of MpesaLib

};

var stkpushquery = await _mpesaClient.QueryLipaNaMpesaTransactionAsync(QueryLipaNaMpesaTransactionObject, accesstoken, equestEndPoint.QueryLipaNaMpesaOnlieTransaction);
```