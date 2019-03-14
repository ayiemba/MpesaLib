
## Account Balance Query Request
```c#
var AccountBalanceObject = new AccountBalanceDto
{	
	IdentifierType = "",// 4 for Paybill 2 for Till
	Initiator = "", //Initiator password
	PartyA = "", 
	QueueTimeOutURL = "",
	ResultURL = "",
	Remarks = "",
	SecurityCredential = "", 
};

var accountbalancerequest = await _mpesaClient.QueryAccountBalanceAsync(AccountBalanceObject, accesstoken, RequestEndPoint.QueryAccountBalance); //async method

var accountbalancerequest = await _mpesaClient.QueryAccountBalance(AccountBalanceObject, accesstoken, RequestEndPoint.QueryAccountBalance); //non-async method

```
