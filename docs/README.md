# MpesaLib 

[![mpesalib MyGet Build Status](https://www.myget.org/BuildSource/Badge/mpesalib?identifier=cf0f8e5c-2a40-41cf-8065-9f27db7e2678)](https://www.myget.org/) [![Build Status](https://geospartan.visualstudio.com/MpesaLib/_apis/build/status/ayiemba.MpesaLib)](https://geospartan.visualstudio.com/MpesaLib/_build/latest?definitionId=2)
 
MPESA API LIBRARY For C# Developers

This documentation is meant to help you get started on how to use this library and does not explain MPESA APIs and there internal workings or exemplifications of when and where you might want to use any of them. If you need in-depth explanation on how Mpesa APIs work you can check **[this](https://peternjeru.co.ke/safdaraja)** well written community site. Otherwise **[Safaricom's Developer Portal](https://developer.safaricom.co.ke/apis-explorer)** should get you all the details you need plus your API keys.

**[Check Sample Code](https://github.com/ayiemba/MpesaLibSamples/blob/master/Apps/WebAppNetCore21/Controllers/HomeController.cs)**

## Setting Up
Before you begin:

1.  Get consumerKey, consumerSecret and Passkey (for Mpesa Express API) from daraja portal liked above by creating an App in their portal.

2.  Ensure your project is running on the latest versions of .Net. I don't intend to support versions before .Net Framework 4.6.1 and .Net Core 2.1. However MpesaLib is based on .Net Standard 2.0 and your are at liberty to check [**here**](https://docs.microsoft.com/en-us/dotnet/standard/net-standard#net-implementation-support) if your platform supports .Net Standard 2.0.

3.  Note that this Library is suited for use through dependency injection (DI). You can read more on DI in Asp.Net core [**here**](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.1). If you don't want to use DI you can always new up MpesaClient by passing in an httpClient instance in the constructor (you have to explicitly provide BaseAdress for the httpClient).

## 1. Registering MpesaClient & Setting BaseAddress
* Install MpesaLib version 2.0.4 and above in your asp.net project (dotnet core >=2.1 or dotnet framework >=4.6.1)
* In **Startup.cs** add the following usings

```c#    
    using MpesaLib.Clients;
    using MpesaLib.Interfaces;
```   

* Inside Configureservices method add the following


```c#
    services.AddHttpClient<IMpesaClient, MpesaClient>(options => options.BaseAddress = new Uri("https://sandbox.safaricom.co.ke/"));
```

*You are now set to use Mpesa APIs in your project via dependency injection. At this point you can inject IMpesaClient in your classes and use it to make Mpesa API calls.*

* You can pass in the MpesaClient in the constructor of your class as follows;
```c#
using MpesaLib.Clients; //required if injecting/instantiating the MpesaClient object
using MpesaLib.Interfaces; // required in injecting the ImpesaClient interface
using MpesaLib.Models;  //Models provides the objects to pass into the methods
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

* To get an accesstoken, invoke the ``` MpesaClient.GetAuthTokenAsync() ``` method. You have to await the call.

e.g. 

```c# 
	var accesstoken = await _mpesaClient.GetAuthTokenAsync(consumerKey, consumerSecret, "oauth/v1/generate?grant_type=client_credentials")
```

Note that you have to pass in a conusmerKey, ConsumerSecret and an end-point Url which is *"oauth/v1/generate?grant_type=client_credentials"* for sandbox. When moving to production use the correct end-point url provided by Safaricom after completing the GO-Live process.

## 3. STK Push (LipaNaMpesaOnline/MpesaExpress)

* All you have to do now is invoke the LipaNaMpesaOnline Method as follows;
```c#
var lipaonline = await _mpesaClient.MakeLipaNaMpesaOnlinePaymentAsync(MpesaExpressObject, accesstoken, "mpesa/stkpush/v1/processrequest");
```
The *MpesaExpressObject* passed in the method contains all the data requred for the request to be processed. See the following sample controller on how to make a LipaNaMpesaOnline request.

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

			//Request accesstoken. The token expire after 1 hr so you should probably implement a caching strategy
            var accesstoken = await _mpesaClient.GetAuthTokenAsync(consumerKey, consumerSecret, "oauth/v1/generate?grant_type=client_credentials");

           
			//Initialize and populate the LipanaMpesa object with required data. The objects come from *`using MpesaLib.Models`* namespace
			var MpesaExpressObject = new LipaNaMpesaOnline
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

## 4. STK Push (LipaNaMpesaOnline/MpesaExpress) Query Request
```c#
var STKPushQueryObject = new LipaNaMpesaQuery
{
	BusinessShortCode = "174379",
	CheckoutRequestID = "",
	Password = "", //this will change in future to use passkey
	Timestamp = "" //this will be taken care of with future release of MpesaLib

};
var stkpushquery = await _mpesaClient.QueryLipaNaMpesaTransactionAsync(STKPushQueryObject, accesstoken, "mpesa/stkpushquery/v1/query");
```

## 5. C2B Register Urls (required for C2B API calls)
```c#
var C2BRegister = new CustomerToBusinessRegister
{
	ConfirmationURL = "https://blablabala/api/confirm",
	ValidationURL = "https://blablabala/api/validate",
	ResponseType = "Cancelled",
	ShortCode = "603047"
};
var c2brequest = await _mpesaClient.RegisterC2BUrlAsync(C2BRegister, accesstoken, "mpesa/c2b/v1/registerurl");
```

## 6. C2B
```c#
//C2B Object
CustomerToBusinessSimulate C2B = new CustomerToBusinessSimulate
{
	ShortCode = "603047",
	Amount = "10",
	BillRefNumber = "account",
	Msisdn = "254708374149",
};

var c2brequest = await _mpesaClient.MakeC2BPaymentAsync(C2B, accesstoken, "mpesa/c2b/v1/simulate");
```

## 7. B2B

```c#
BusinessToBusiness B2B = new BusinessToBusiness
{
	AccountReference = "test",
	Initiator = "safaricom.13",
	Amount = "1",
	PartyA = "603047",
	PartyB = "600000",
	CommandID = "MerchantToMerchantTransfer",// Please chack the correct command from Daraja
	QueueTimeOutURL = "https://blablabala/callback",
	RecieverIdentifierType = "4",
	SecurityCredential = B2BsecurityCred, // See #12 on how to get security credential
	SenderIdentifierType = "4",
	ResultURL = "https://blablabala/callback",
	Remarks = "payment"
};
var b2brequest = await _mpesaClient.MakeB2BPaymentAsync(B2B, accesstoken, "mpesa/b2b/v1/paymentrequest");
```
## 8. B2C
```c#
//B2C Object
BusinessToCustomer B2C = new BusinessToCustomer
{
	Remarks = "test",
	Amount = "1",
	CommandID = "BusinessPayment",
	InitiatorName = "safaricom.15",
	Occasion = "test",
	PartyA = "603047",
	PartyB = "254708374149",
	QueueTimeOutURL = "https://blablabala/callback",
	ResultURL = "https://blablabala/callback",
	SecurityCredential = B2CsecurityCred //see #12 below on how to get security credential
};
var b2crequest = await _mpesaClient.MakeB2CPaymentAsync(B2C, accesstoken, "mpesa/b2c/v1/paymentrequest");
```

## 9. Account Balance Query
```c#
var AccountBalance = new AccountBalance
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
var accountbalancerequest = await _mpesaClient.QueryAccountBalanceAsync(AccountBalance, accesstoken, "mpesa/accountbalance/v1/query");
```

## 10. Transaction Status
```c#
var TransactionStatus = new MpesaTransactionStatus
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
var transactionrequest = await _mpesaClient.QueryMpesaTransactionStatusAsync(TransactionStatus, accesstoken, "mpesa/transactionstatus/v1/query");
```

## 11. Transaction Reversal
```c#
var TransactionReversal = new Reversal
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
var reversalrequest = await _mpesaClient.ReverseMpesaTransactionAsync(TransactionReversal, accesstoken, "mpesa/reversal/v1/request");
```
## 12. Getting Security Credential for B2B, B2C, Reversal, Transaction Status and Account Balance APIs
The Security Credential helper is found in MpesaLib.Helpers.Credential.
All you have to do is call 

```c# 
Credentials.EncryptPassword(pathToSafaricomPublicCertificate, YourInitiatorpassword)
```

```c#
using MpesaLib.Helpers; // Add this to your class

//set path of Mpesa public certificate
 string certificate = @"C:\Dev\Work\MpesaIntegration\MpesaLibSamples\WebApplication1\Certificate\prod.cer";
 
 //set security credential as follows...
 var SecutityCredential = Credentials.EncryptPassword(certificate, "971796");

```

