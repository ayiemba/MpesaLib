
## Account Balance Query Request
```c#
var AccountBalanceObject = new AccountBalanceDto
{	
	IdentifierType = "",
	Initiator = "",
	PartyA = "",
	QueueTimeOutURL = "",
	ResultURL = "",
	Remarks = "",
	SecurityCredential = "", 
};
var accountbalancerequest = await _mpesaClient.QueryAccountBalanceAsync(AccountBalanceObject, accesstoken, "mpesa/accountbalance/v1/query");
```
