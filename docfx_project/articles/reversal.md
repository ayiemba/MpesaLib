
## Transaction Reversal Request
```c#
var TransactionReversalObject = new ReversalDto
{
	Initiator = "",	
	Occasion = "",
	ReceiverParty = "",
	RecieverIdentifierType = "",
	QueueTimeOutURL = "",
	ResultURL = "",
	TransactionID = "",
	SecurityCredential = "", 
	Remarks = "",

};
var reversalrequest = await _mpesaClient.ReverseMpesaTransactionAsync(TransactionReversalObject, accesstoken, "mpesa/reversal/v1/request");
```
