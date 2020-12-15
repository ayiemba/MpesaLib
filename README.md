# MpesaLib 
[![Build Status](https://geospartan.visualstudio.com/MpesaLib/_apis/build/status/ayiemba.MpesaLib)](https://geospartan.visualstudio.com/MpesaLib/_build/latest?definitionId=2)
[![NuGet version (MpesaLib)](https://img.shields.io/nuget/v/MpesaLib.svg?style=flat-square)](https://www.nuget.org/packages/MpesaLib/)
 
A .NET Standard M-PESA API Helper Library for .NET Developers.
- [End User License](https://github.com/ayiemba/MpesaLib/blob/master/LICENSE)
- [Release Notes](https://github.com/ayiemba/MpesaLib/releases)
- [NuGet Package](https://www.nuget.org/packages/MpesaLib/)
- [Roadmap](https://github.com/ayiemba/MpesaLib/blob/master/docfx_project/articles/roadmap.md)
- [Contributing Guidelines](https://github.com/ayiemba/MpesaLib/blob/master/docfx_project/articles/contributing.md)
- [Mpesa Daraja Portal](https://developer.safaricom.co.ke/)

## Supported Platforms

|   *Platform*   | .NET Core | .NET Framework | Mono | Xamarin.iOS | Xamarin.Android | Xamarin.Mac |     UWP    |
|:------------:|:---------:|:--------------:|:----:|:-----------:|:---------------:|:-----------:|:----------:|
| *Min. Version* |    2.0    |      4.6.1     |  5.4 |    10.14    |       8.0       |     3.8     | 10.0.16299 |

## Installation
- ```PM> Install-Package MpesaLib```
- ```>dotnet add package MpesaLib```

## Setting yourself up for success
Before you proceed kindly aquaint yourself with Mpesa Apis by going through the Docs in Safaricom's developer portal or Daraja if you like.

1.  Obtain consumerKey, consumerSecret and Passkey (for Lipa Na Mpesa Online APIs) from daraja portal.

2.  Ensure your project is running on the minimun supported versions of .Net 

3.  MpesaLib is dependency injection (DI) friendly and can be readily injected into your classes. You can read more on DI in Asp.Net core [**here**](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.1). If you can't use DI you can always manually create a new instance of MpesaClient and pass in an httpClient instance in it's constructor. eg.

```c#
// When DI is not possible for your case don't give up just yet...

//create httpclient instance
var httpClient = new HttpClient();

httpClient.BaseAddress = RequestEndPoint.SandboxBaseAddress; //Use RequestEndPoint.LiveBaseAddress in production
	
//create Mpesa API client instance
var mpesaClient = new MpesaClient(httpClient); //make sure to pass httpclient intance as an argument
	
```
I would recommend the DI way of doing things though...

## Registering MpesaClient & Set the BaseAddress -Dependency Injection Method
* Install MpesaLib .Net Project via Nuget Package Manager Console or Nuget Package Manager GUI.

* In **Startup.cs** add the namespace...

```c#    
using MpesaLib;
```

* Inside ConfigureServices method add the following

```c#
services.AddHttpClient<IMpesaClient, MpesaClient>(options => options.BaseAddress = RequestEndPoint.SandboxBaseAddress);
```

Use ```RequestEndPoint.LiveBaseAddress``` as base address/base url in production. You can do an environment check using the IHostingEnvironment property in asp.net core.

* Once the MpesaClient is registered, you can pass it and use it in your classes to make API calls to Mpesa Server as follows;
```c#
using MpesaLib; //Add MpesaLib namespace
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

## Requesting for the Accesstoken
Mpesa APIs require authorization to use the APIs. The accesstoken (auth token) has to be used with each api call. The accesstoken expire after an hour so it is recommended that you use a caching strategy to refresh the token after every hour or less depending on how  much traffic your site has.

* To get an accesstoken, invoke the ``` _mpesaClient.GetAuthTokenAsync(*args); ``` method. You have to await the Async call. use Non-Async method call provided if you cannot leverage async.

```c# 
//Async 
var accesstoken = await _mpesaClient.GetAuthTokenAsync(ConsumerKey, ConsumerSecret, RequestEndPoint.AuthToken);

```

Note that you have to pass in a consusmerKey, ConsumerSecret provided by Mpesa.


## C2B Register Urls Request
```c#
var RegisterC2BUrlObject = new CustomerToBusinessRegisterUrlDto(
	"ShortCode",
    	"ResponseType",
    	"ConfirmationURL",
    	"ValidationURL"
);

var c2bRegisterUrlrequest = await _mpesaClient.RegisterC2BUrlAsync(RegisterC2BUrlObject, accesstoken, RequestEndPoint.RegisterC2BUrl);
```

## C2B Payment Request
```c#
//C2B Object
Var CustomerToBusinessSimulateObject = new CustomerToBusinessSimulateDto
(
	"ShortCode",
    	"CommandID",
    	"Amount",
    	"Msisdn",
    	"BillRefNumber"
);

var c2brequest = await _mpesaClient.MakeC2BPaymentAsync(CustomerToBusinessSimulateObject, accesstoken, RequestEndPoint.CustomerToBusinessSimulate);
```

## LipaNaMpesaOnline/MpesaExpress (STK Push) Payment Request

```c#
// initialize object with data
var MpesaExpressObject = new LipaNaMpesaOnlineDto
(
	"BusinessShortCode",// businessShortCode
    	Timestamp, //Timestamp
        "TransactionType",  //transactionType
        "Amount", // amount
        "PartyA" ,// partyA
        "PartyB" ,// partyB
        "PhoneNumber", // phoneNumber
        "CallBackURL", // callBackUrl
        "AccountReference" ,//accountReference
        "TransactionDesc" ,//transactionDescription
        "Passkey" //passkey
);

//Make payment request 
var paymentrequest = await _mpesaClient.MakeLipaNaMpesaOnlinePaymentAsync(MpesaExpressObject, accesstoken, RequestEndPoint.LipaNaMpesaOnline));

```

## LipaNaMpesaOnline/MpesaExpress Transaction Query Request
```c#
var QueryLipaNaMpesaTransactionObject = new LipaNaMpesaQueryDto
(
	 "174379", //BusinessShortCode
	 "CheckoutRequestID", //CheckoutRequestID
	 "Password", //Password
	 "Timestamp" //Timestamp

);

var stkpushquery = await _mpesaClient.QueryLipaNaMpesaTransactionAsync(QueryLipaNaMpesaTransactionObject, accesstoken, equestEndPoint.QueryLipaNaMpesaOnlieTransaction);
```

## B2C Payment Request
```c#
//B2C Object
var BusinessToCustomerObject = new BusinessToCustomerDto
(
	"test",//Remarks
	"1", //Amount
	"BusinessPayment", //CommandID
	"safaricom.15", //InitiatorName
	"test", //Occasion
	"603047", //PartyA
	"254708374149", //PartyB
	"https://blablabla/timeoutendpoint", //QueueTimeOutURL
	"https://blablabla/resultendpoint", //ResultURL
	"security credential" //SecurityCredential
);

var b2crequest = await _mpesaClient.MakeB2CPaymentAsync(BusinessToCustomerObject, accesstoken, RequestEndPoint.BusinessToCustomer);

```

## B2B Payment Request

```c#
var BusinessToBusinessObject = new BusinessToBusinessDto
(
	"test", //AccountReference 
	"safaricom.13", //Initiator 
	"1500", //Amount 
	"603047", //PartyA 
	"600000", //PartyB
	"BusinessPayBill",// Please use the correct command -usage depends on what is enabled for your shortcode
	"https://blablabla/callback", //QueueTimeOutURL 
	"4", // RecieverIdentifierType  - Read on receiver identifier types from daraja
	"security credential", //SecurityCredential - Use MpesaLib.Helpers.Credentials class to generate security credential
	"4", //SenderIdentifierType
	"https://blablabla/callback", //ResultURL
	"payment" //Remarks
);

var b2brequest = await _mpesaClient.MakeB2BPaymentAsync(BusinessToBusinessObject, accesstoken, RequestEndPoint.BusinessToBusiness);

```

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

## Account Balance Query Request
```c#
var AccountBalanceObject = new AccountBalanceDto
(	
	"IdentifierType",// 4 for Paybill 2 for Till
	"Initiator", //Initiator password
	"PartyA", 
	"QueueTimeOutURL",
	"ResultURL",
	"remarks",
	"security credential", 
);

var accountbalancerequest = await _mpesaClient.QueryAccountBalanceAsync(AccountBalanceObject, accesstoken, RequestEndPoint.QueryAccountBalance); //async method

```

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

## Getting Security Credential for B2B, B2C, Reversal, Transaction Status and Account Balance APIs
The Security Credential helper class is in MpesaLib.Helpers namespace.

This class helps you generate the required credential to be used to authorize the above mentioned APIs.

```c#
using MpesaLib.Helpers; // add this to your class or namespace

//get path to Mpesa public certificate. There are different certs for development and for production, ensure to use the correct one)

string certificate = @"C:\Dev\MpesaLibSamples\WebApplication1\Certificate\prod.cer";
 
 //generate security credential as follows...

var SecutityCredential = Credentials.EncryptPassword(certificate, "Initiator Password");

```

## Error handling
MpesaClient Throws ```MpesaApiException``` whenever A 200 status code is not returned. It is your role as the developer to catch
the exception and continue processing in your aplication. Snippet below shows how you can catch the MpesaApiException.

```c#
using MpesaLib.Helpers.Exceptions; // add this to you class or namespace


try
{	
	return await _mpesaClient.MakeLipaNaMpesaOnlinePaymentAsync(MpesaPayment, accesstoken, RequestEndPoint.LipaNaMpesaOnline);
}
catch (MpesaApiException e)
{
	_logger.LogError($"An Error Occured, Status Code {e.StatusCode}: {e.Content}");

//check the status code and return what is appropriate for your case. e.Content has the error message from Mpesa inform of a json string (not object) incase you do things like tying to transact 0 shillings or more than the 70k limit per transaction, or your shorcode is wrong or your accesstoken is not valid etc.
    
	return BadRequest(); //I am just being lazy here.
}
			
```

