

## Transaction Status Request
```c#
var TransactionStatusObject = new MpesaTransactionStatusDto
(
	 "IdentifierType",//IdentifierType
	 "Initiator", //Initiator
	 "Occasion", //Occasion
	 "PartyA", //PartyA
	 "QueueTimeOutURL", //QueueTimeOutURL
	 "ResultURL", //ResultURL
	 "TransactionID", //TransactionID
	 "Remarks", //Remarks
	 "SecurityCredential" //SecurityCredential
);

var transactionrequest = await _mpesaClient.QueryMpesaTransactionStatusAsync(TransactionStatusObject, accesstoken, RequestEndPoint.QueryMpesaTransactionStatus);
```
