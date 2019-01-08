
## Intalling
- ```PM> Install-Package MpesaLib```
- ```>dotnet add package MpesaLib```

## Setting Up
Before you proceed kindly aquaint yourself with Mpesa Apis by going through the Docs in Safaricom's developer portal linked above.

1.  Obtain consumerKey, consumerSecret and Passkey (for STK PUsh APIs) from daraja portal linked above by creating your App.

2.  Ensure your project is running on the latest versions of .Net. This library does not support .Net versions before .Net Framework 4.6.1 and .Net Core 2.1. However, MpesaLib is based on .Net Standard 2.0 and your are at liberty to check [**here**](https://docs.microsoft.com/en-us/dotnet/standard/net-standard#net-implementation-support) if your platform supports .Net Standard 2.0.

3.  MpesaLib is dependency injection (DI) friendly and can be readily injected into your classes. You can read more on DI in Asp.Net core [**here**](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.1). If you can't use DI you can always manually create a new instance of MpesaClient and pass in an httpClient instance in it's constructor (you have to explicitly provide the BaseAdress for the same). eg.

```c#
	//Use only when you can't use Dependency injection
	//create httpclient instance
	var httpClient = new HttpClient();
	httpClient.BaseAddress = new Uri("https://sandbox.safaricom.co.ke/");
	
	//create Mpesa API client instance
	var mpesaClient = new MpesaClient(httpClient); //note how httpClient instance is passed into MpesaClient as a parameter.
	
```
These Documentation recommends the DI way of doing things.

## How to Register MpesaClient & Set the BaseAddress
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

## Getting an accesstoken
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



