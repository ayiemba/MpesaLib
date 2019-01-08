
## Transaction Reversal Request
```c#
var TransactionReversalObject = new ReversalDto
{
	Initiator = "",
	Amount = "",
	Occasion = "",
	ReceiverParty = "",
	RecieverIdentifierType = "",
	QueueTimeOutURL = "",
	ResultURL = "",
	TransactionID = "",
	SecurityCredential = "", //see #12 below on how to get security credential
	Remarks = "",

};
var reversalrequest = await _mpesaClient.ReverseMpesaTransactionAsync(TransactionReversalObject, accesstoken, "mpesa/reversal/v1/request");
```