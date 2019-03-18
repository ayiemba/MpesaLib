

## LipaNaMpesaOnline/MpesaExpress Transaction Query Request
```c#
var QueryLipaNaMpesaTransactionObject = new LipaNaMpesaQueryDto
{
	BusinessShortCode = "174379",
	CheckoutRequestID = "",
	Password = "",
	Timestamp = "" 

};

var stkpushquery = await _mpesaClient.QueryLipaNaMpesaTransactionAsync(QueryLipaNaMpesaTransactionObject, accesstoken, equestEndPoint.QueryLipaNaMpesaOnlieTransaction);
```