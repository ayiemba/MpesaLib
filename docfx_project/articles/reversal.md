
## Transaction Reversal Request
```c#
var TransactionReversalObject = new ReversalDto
(
	"Initiator",	//Initiator
	"Occasion", //Occasion
	"ReceiverParty", //ReceiverParty
	"RecieverIdentifierType", //RecieverIdentifierType
	"QueueTimeOutURL", //QueueTimeOutURL
	"ResultURL", //ResultURL
	"TransactionID", //TransactionID
	"SecurityCredential",  //SecurityCredential
	"Remarks", //Remarks

);

var reversalrequest = await _mpesaClient.ReverseMpesaTransactionAsync(TransactionReversalObject, accesstoken, RequestEndPoint.ReverseMpesaTransaction);

```
