# MpesaLib 

[![mpesalib MyGet Build Status](https://www.myget.org/BuildSource/Badge/mpesalib?identifier=cf0f8e5c-2a40-41cf-8065-9f27db7e2678)](https://www.myget.org/) [![Build Status](https://geospartan.visualstudio.com/MpesaLib/_apis/build/status/ayiemba.MpesaLib)](https://geospartan.visualstudio.com/MpesaLib/_build/latest?definitionId=2)
[![NuGet version (MpesaLib)](https://img.shields.io/nuget/v/MpesaLib.svg?style=flat-square)](https://www.nuget.org/packages/MpesaLib/)
 
MPESA API LIBRARY For C# Developers

This documentation is meant to help you get started on how to use this library and does not explain MPESA APIs and their internal workings or exemplifications of when and where you might want to use any of them. If you need in-depth explanation on how Mpesa APIs work you can check **[this](https://peternjeru.co.ke/safdaraja)** well written community site. Otherwise **[Safaricom's Developer Portal](https://developer.safaricom.co.ke/apis-explorer)** should get you all the details you need plus your API keys to get started.

## Note that MpesaLib Version 3.x.x comes with breaking changes and the documentation has been updated to capture the changes.


## Setting Up
Before you proceed aquaint yourself with Mpesa Apis by going through the Docs in Safaricom's developer portal linked above:

1.  Get consumerKey, consumerSecret and Passkey (for STK PUsh APIs) from daraja portal linked above by creating your App.

2.  Ensure your project is running on the latest versions of .Net. I don't intend to provide support for versions before .Net Framework 4.6.1 and .Net Core 2.1. However MpesaLib is based on .Net Standard 2.0 and your are at liberty to check [**here**](https://docs.microsoft.com/en-us/dotnet/standard/net-standard#net-implementation-support) if your platform supports .Net Standard 2.0.

3.  Note that this Library is suitable for use through dependency injection (DI). You can read more on DI in Asp.Net core [**here**](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.1). If you don't want to use DI you can always new-up MpesaClient and pass in an httpClient instance in the constructor (you have to explicitly provide the BaseAdress). eg.

```c#
	//Use only when you can't use Dependency injection
	//create httpclient instance
	var httpClient = new HttpClient();
	httpClient.BaseAddress = new Uri("https://sandbox.safaricom.co.ke/");
	
	//create Mpesa API client instance
	var mpesaClient = new MpesaClient(httpClient); //note how httpClient instance is passed into MpesaClient as a parameter.
	
```
These Documentation recommends the DI way of doing things.

## 1. How to Register MpesaClient & Set the BaseAddress
* Install MpesaLib .Net Project (dotnet core >= 2.1 or dotnet framework >= 4.6.1)
*Install-Package MpesaLib -Version 3.0.132 

In visual Studio, under Manage Nuget Packages, search for and Install MpesaLib.

* In **Startup.cs** add the namespace...

```c#    
    using MpesaLib;
```   

* Inside Configureservices method add the following

```c#
    services.AddHttpClient<IMpesaClient, MpesaClient>(options => options.BaseAddress = new Uri("https://sandbox.safaricom.co.ke/"));
```

*When going live replace "https://sandbox.safaricom.co.ke/" with "https://api.safaricom.co.ke/"
*Or even better, use ```RequestEndPoint.SandboxBaseAddress```  or ```RequestEndPoint.LiveBaseAddress```



* You can pass in the MpesaClient in the constructor of your class as follows;
```c#
using MpesaLib; //MpesaLib namespace
public class Payments
{
	private readonly IMpesaClient _mpesaClient;
	public Payments(IMpesaCleint mpesaClient)
	{
		_mpesaClient = mpesaClient;
	}
	....
	//code omitted for brevity
}
```

## 2. Getting an accesstoken
Mpesa APIs require an accesstoken for authentication/authorization to use the APIs. The accesstoken has to be passed into the available api method calls. MpesaLib provides two methods (asyncronous and non-asyncronous) for requesting an accesstoken. Currently only asynchronous method is supported by the library for all API calls. The accesstokens expire after an hour so it is recommended that you implement a caching strategy that refereshes the token after every hour or less.

* To get an accesstoken, invoke the ``` _mpesaClient.GetAuthTokenAsync(*args); ``` method. You have to await the Async call. use Non-Async call if method is not async.

e.g. 

```c# 
	//Async 
	var accesstoken = await _mpesaClient.GetAuthTokenAsync(consumerKey, consumerSecret, "oauth/v1/generate?grant_type=client_credentials");
	
	//Non-Async 
	var accesstoken = _mpesaClient.GetAuthTokenAsync(consumerKey, consumerSecret, "oauth/v1/generate?grant_type=client_credentials").GetAwaiter().GetResult();
```

Note that you have to pass in a conusmerKey, ConsumerSecret and an end-point Url which is *"oauth/v1/generate?grant_type=client_credentials"* for sandbox. When moving to production use the correct end-point url provided by Safaricom after completing the GO-Live process.

## 3. LipaNaMpesaOnline/MpesaExpress (STK Push) Payment Request



* In your payment class or controller inject IMpesaClient interface and use it to make API calls. I also inject Iconfiguration since i store my keys in a configuration file

```c#

public class PaymentsController : Controller
{          
        private readonly IConfiguration _config;          
		private readonly IMpesaClient _mpesaClient;

	public PaymentsController(IMpesaClient mpesaClient,IConfiguration configuration)
	{
		_mpesaClient = mpesaClient;
		_config = configuration;

	}
        [HttpPost] 
        [Route("/make-payment")]
        public async Task<IActionResult> PayWithMpesa(PaymentViewModel Payment)
        {
            var consumerKey = _config["MpesaConfiguration:ConsumerKey"];

            var consumerSecret = _config["MpesaConfiguration:ConsumerSecret"];

			var passKey = _config["MpesaConfiguration:PassKey"];

			//Request accesstoken. The token expire after 1 hour so you should probably have a caching strategy in place
            var accesstoken = await _mpesaClient.GetAuthTokenAsync(consumerKey, consumerSecret, "oauth/v1/generate?grant_type=client_credentials");

           
			//Initialize and populate the LipanaMpesa object with required data. The objects come from *`using MpesaLib.Models`* namespace
			var MpesaExpressObject = new LipaNaMpesaOnlineDto
			{
				AccountReference = "ref",
				Amount = Payment.PendingRate,
				PartyA = Payment.PhoneNumber,
				PartyB = "174379",
				BusinessShortCode = "174379",
				CallBackURL = "https://mydomain.co.ke/api/callback", // create your own callback url
				Passkey = passKey,	//get passkey from daraja			  
				PhoneNumber = Payment.PhoneNumber, 				
				TransactionDesc = "test"
				//Note that you don't have to provide password and timestamp, i calculate that for you automatically provide you give passkey. You can overwrite those properties if you wish to.					
			};

			//Make payment request 
			var paymentrequest = await _mpesaClient.MakeLipaNaMpesaOnlinePaymentAsync(MpesaExpressObject, accesstoken, "mpesa/stkpush/v1/processrequest");

            
            return View();
        }

    }

```

## 4. LipaNaMpesaOnline/MpesaExpress Transaction Query Request
```c#
var QueryLipaNaMpesaTransactionObject = new LipaNaMpesaQueryDto
{
	BusinessShortCode = "174379",
	CheckoutRequestID = "",
	Password = "", //this will change in future to use passkey
	Timestamp = "" //this will be taken care of with future release of MpesaLib

};
var stkpushquery = await _mpesaClient.QueryLipaNaMpesaTransactionAsync(QueryLipaNaMpesaTransactionObject, accesstoken, "mpesa/stkpushquery/v1/query");
```

## 5. C2B Register Urls Request
```c#
var RegisterC2BUrlObject = new CustomerToBusinessRegisterUrlDto
{
	ConfirmationURL = "https://blablabala/api/confirm",
	ValidationURL = "https://blablabala/api/validate",
	ResponseType = "Cancelled",
	ShortCode = "603047"
};
var c2bRegisterUrlrequest = await _mpesaClient.RegisterC2BUrlAsync(RegisterC2BUrlObject, accesstoken, "mpesa/c2b/v1/registerurl");
```

## 6. C2B Payment Request
```c#
//C2B Object
Var CustomerToBusinessSimulateObject = new CustomerToBusinessSimulateDto
{
	ShortCode = "603047",
	Amount = "10",
	BillRefNumber = "account",
	Msisdn = "254708374149",
	CommandID = "CustomerPayBillOnline"
};

var c2brequest = await _mpesaClient.MakeC2BPaymentAsync(CustomerToBusinessSimulateObject, accesstoken, "mpesa/c2b/v1/simulate");
```

## 7. B2B Payment Request

```c#
var BusinessToBusinessObject = new BusinessToBusinessDto
{
	AccountReference = "test",
	Initiator = "safaricom.13",
	Amount = "1",
	PartyA = "603047",
	PartyB = "600000",
	CommandID = "MerchantToMerchantTransfer",// Please chack the correct command from Daraja
	QueueTimeOutURL = "https://blablabala/callback",
	RecieverIdentifierType = "4", //Read on identifier types from daraja
	SecurityCredential = B2BsecurityCred, // See #12 on how to generate security credential
	SenderIdentifierType = "4",
	ResultURL = "https://blablabala/callback",
	Remarks = "payment"
};
var b2brequest = await _mpesaClient.MakeB2BPaymentAsync(BusinessToBusinessObject, accesstoken, "mpesa/b2b/v1/paymentrequest");
```
## 8. B2C Payment Request
```c#
//B2C Object
var BusinessToCustomerObject = new BusinessToCustomer
{
	Remarks = "test",
	Amount = "1",
	CommandID = "BusinessPayment",
	InitiatorName = "safaricom.15",
	Occasion = "test",
	PartyA = "603047",
	PartyB = "254708374149",
	QueueTimeOutURL = "https://blablabala/timeoutendpoint",
	ResultURL = "https://blablabala/resultendpoint",
	SecurityCredential = B2CsecurityCred //see #12 below on how to get security credential
};
var b2crequest = await _mpesaClient.MakeB2CPaymentAsync(BusinessToCustomerObject, accesstoken, "mpesa/b2c/v1/paymentrequest");
```

## 9. Account Balance Query Request
```c#
var AccountBalanceObject = new AccountBalanceDto
{
	Amount = "",
	IdentifierType = "",
	Initiator = "",
	PartyA = "",
	QueueTimeOutURL = "",
	ResultURL = "",
	Remarks = "",
	SecurityCredential = "", //see #12 below on how to get security credential
};
var accountbalancerequest = await _mpesaClient.QueryAccountBalanceAsync(AccountBalanceObject, accesstoken, "mpesa/accountbalance/v1/query");
```

## 10. Transaction Status Request
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

## 11. Transaction Reversal Request
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
## 12. Getting Security Credential for B2B, B2C, Reversal, Transaction Status and Account Balance APIs
The Security Credential helper is found in MpesaLib.Helpers.Credential.
All you have to do is call 

```c# 
Credentials.EncryptPassword(pathToSafaricomPublicCertificate, YourInitiatorpassword)
```
e.g
```c#
using MpesaLib.Helpers; // Add this to your class

//get path of Mpesa public certificate
 string certificate = @"C:\Dev\Work\MpesaIntegration\MpesaLibSamples\WebApplication1\Certificate\prod.cer";
 
 //generate security credential as follows...
 var SecutityCredential = Credentials.EncryptPassword(certificate, "971796");

```

## 13. Async vs Sync Tips
** You can use ```_mpesaClient.GetAuthToken(*args).GetAwaiter().GetResult();``` instead of ```await _mpesaClient.GetAuthToken(*args);```  when for one reason or another you can't use asynchronous methods.

## 14. Magic Strings
From version 3.X.X of MpesaLib you can avoid having to put magic strings in your method calls by taking advantage of the ```MpesaLib.RequestEndPoint``` class. The RequestEndPoint Class defines all the string for you so you don't have to worry about getting an endpoint wrong. For example now you do the following..
```await _mpesaClient.ReverseMpesaTransactionAsync(TransactionReversalObject, accesstoken, RequestEndPoint.ReverseMpesaTransaction");```
instead of...
```await _mpesaClient.ReverseMpesaTransactionAsync(TransactionReversalObject, accesstoken, "mpesa/reversal/v1/request");```

The same when setting BaseAdress...
For Sandbox:
```services.AddHttpClient<IMpesaClient, MpesaClient>(opts=>opts.BaseAddress = RequestEndPoint.SandboxBaseAdress);```
For Live API:
```services.AddHttpClient<IMpesaClient, MpesaClient>(opts=>opts.BaseAddress = RequestEndPoint.LiveBaseAdress);```



Be warned, the following samples are not up to date!

**[Check Samples](https://github.com/ayiemba/MpesaLibSamples/blob/master/Apps/WebAppNetCore21/Controllers/HomeController.cs)**


