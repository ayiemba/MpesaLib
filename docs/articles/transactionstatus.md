

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
	SecurityCredential = "" //see #12 below on how to get security credential
};
var transactionrequest = await _mpesaClient.QueryMpesaTransactionStatusAsync(TransactionStatusObject, accesstoken, "mpesa/transactionstatus/v1/query");
```