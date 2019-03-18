
## Installation
- ```PM> Install-Package MpesaLib```
- ```>dotnet add package MpesaLib```

## Setting Up
Before you proceed kindly aquaint yourself with Mpesa Apis by going through the Docs in Safaricom's developer portal or Daraja if you like.

1.  Obtain consumerKey, consumerSecret and Passkey (for Lipa Na Mpesa Online APIs) from daraja portal.

2.  Ensure your project is running on the minimun supported versions of .Net 

3.  MpesaLib is dependency injection (DI) friendly and can be readily injected into your classes. You can read more on DI in Asp.Net core [**here**](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.1). If you can't use DI you can always manually create a new instance of MpesaClient and pass in an httpClient instance in it's constructor. eg.

```c#
//Use only when you can't use Dependency injection

//create httpclient instance
var httpClient = new HttpClient();

httpClient.BaseAddress = RequestEndPoint.SandboxBaseAddress; //Use RequestEndPoint.LiveBaseAddress in production
	
//create Mpesa API client instance
var mpesaClient = new MpesaClient(httpClient); //make sure to pass httpclient intance as an argument
	
```
These Documentation recommends the DI way of doing things. Sorry if that's  opinionated.

## How to Register MpesaClient & Set the BaseAddress
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

## Getting an accesstoken
Mpesa APIs require an accesstoken for authentication/authorization to use the APIs. The accesstoken has to be passed into the available api method calls. MpesaLib provides two methods (asyncronous and non-asyncronous) for requesting an accesstoken. The accesstokens expire after an hour so it is recommended that you implement a caching strategy that refreshes the token after every hour or less depending on how  much traffic your site has...you don't want to suffocate Mpesa Server with needless API calls!

* To get an accesstoken, invoke the ``` _mpesaClient.GetAuthTokenAsync(*args); ``` method. You have to await the Async call. use Non-Async call if you prefer prefer synchronous way of doing things in your app. e.g. 

```c# 
//Async 
var accesstoken = await _mpesaClient.GetAuthTokenAsync(ConsumerKey, ConsumerSecret, RequestEndPoint.AuthToken);

//Non-Async (Avoid non-asyn calls if you can)
var accesstoken = await _mpesaClient.GetAuthToken(ConsumerKey, ConsumerSecret, RequestEndPoint.AuthToken);
```

Note that you have to pass in a conusmerKey, ConsumerSecret from daraja.



