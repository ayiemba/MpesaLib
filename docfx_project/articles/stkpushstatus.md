

## LipaNaMpesaOnline/MpesaExpress Transaction Query Request
```c#
var QueryLipaNaMpesaTransactionObject = new LipaNaMpesaQueryDto
(
	 "174379", //BusinessShortCode
	 "CheckoutRequestID", //CheckoutRequestID
	 "Password", //Password
	 "Timestamp" //Timestamp

);

var stkpushquery = await _mpesaClient.QueryLipaNaMpesaTransactionAsync(QueryLipaNaMpesaTransactionObject, accesstoken, equestEndPoint.QueryLipaNaMpesaOnlieTransaction);
```
