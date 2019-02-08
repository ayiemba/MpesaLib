

## Transaction Status Request
```c#
var TransactionStatusObject = new MpesaTransactionStatusDto
{
	IdentifierType = "",
	Initiator = "",
	Occasion = "",
	PartyA = "",
	QueueTimeOutURL = "",
	ResultURL = "",
	TransactionID = "",
	Remarks = "",
	SecurityCredential = ""
};

var transactionrequest = await _mpesaClient.QueryMpesaTransactionStatusAsync(TransactionStatusObject, accesstoken, RequestEndPoint.QueryMpesaTransactionStatus);
```