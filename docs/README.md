# MpesaLib [![mpesalib MyGet Build Status](https://www.myget.org/BuildSource/Badge/mpesalib?identifier=cf0f8e5c-2a40-41cf-8065-9f27db7e2678)](https://www.myget.org/)
 
MPESA API LIBRARY For C# Developers

This documentation is meant to help you get started on how to use this library and does not explain MPESA APIs and there internal workings or exemplifications of when and where you might want to use any of them. If you need in-depth explanation on how Mpesa APIs work you can check **[this](https://peternjeru.co.ke/safdaraja)** well written community site. Otherwise **[Safaricom's Developer Portal](https://developer.safaricom.co.ke/apis-explorer)** should get you all the details you need plus your API keys.

## Set Up
Before you begin:

1.  Get consumerKey, consumerSecret and Passkey (for Mpesa Express API) from daraja portal liked above by creating an App in their portal.
2.  Ensure your project is running on the latest versions of .Net. I don't intend to support versions before .Net Framework 4.6.1 and .Net Core 2.1. However MpesaLib is based on .Net Standard 2.0 and your are at liberty to check [**here**](https://docs.microsoft.com/en-us/dotnet/standard/net-standard#net-implementation-support) if your platform is supported by .Net Standard 2.0.
3.  Note that this Library is suited for use through dependency injection (DI). You can read more on DI in Asp.Net core [**here**](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.1). You don't have to use the default .Net DI system,you can use the one you prefer.

For now i'll just show how to do Mpesa Express or LipaNaMpesaOnline. The principle applies for the other API's, the only difference is the object passed into the API request body. I will make effort to provide documentation on how to use the others but if you can do this, you can figure out the rest. The library supports all the Mpesa APIs in daraja.

## 1. STK-Push / Mpesa Express/ LipaNaMpesa Online

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






## 2. C2B - Docs Coming soon
## 3. B2B - Docs Coming soon
## 4. B2C - Docs Coming soon
## 5. Transaction Reversal - Docs Coming soon




### If using MpesaLib 2.0.0 and below see the following section. This is still supported in MpesaLib 2.0.4 but might be removed in future versions of the Library. Use above sections to interact with the Mpesa APIs.The difference in the above method and the below method is in the re-use of httpClient. The above reuses the same httpclient instance for all the APIs while the methods below create a httpclient instance for each API - this is risky for high traffic applications (You risk socket exhaution). Also the below sections are not straight forward on how to set the baseAdress. In the above method the BaseAdress in set once and you can easily swap sandbox address and production address. The above methods also force you to explicitly provide the API endpoint URl.

.
.
.
.
.
.
.





## HOW TO USE In an ASP.NET Core Web Application

* Run `Install-Package MpesaLib -Version 1.X.X` in Package Manager Console or go to Manage Nuget Packages, Search and install MpesaLib
* Add usings - these are the only namespaces you'll ever need from MpesaLib

```c# 
using MpesaLib.Clients; //gives you the clients
using MpesaLib.Interfaces; //gives you the interfaces for use in DI
using MpesaLib.Models; //gives the DTOs for each client

```
### Option 1 on how to add the services using Dependency Injection: 
* Add Mpesa API Clients in DI Container; For asp.net core core this can be done in Startup.cs.
There are about 10 mpesa api clients. Only register and use the specific ones you need in your code. If you need more than 3 clients follow Option 2 to add them in startup.cs in a cleaner way.

```c#
    //Add AuthClient - gets you accesstokens (This is manadatory)
    services.AddHttpClient<IAuthClient, AuthClient>();
    
    //Add LipaNaMpesaOnlineClient - makes STK Push payment requests
    services.AddHttpClient<ILipaNaMpesaOnlineClient, LipaNaMpesaOnlineClient>();
    
    //Add C2BRegisterUrlClient - register your callback URLS, goes hand-in-hand with the C2BClient
    services.AddHttpClient<IC2BRegisterUrlClient, C2BRegisterUrlClient>();
    
    //Add C2BClient - makes customer to business payment requests 
    services.AddHttpClient<IC2BClient, C2BClient>();
    
    //Add B2BClient - makes business to business payment requests
    services.AddHttpClient<IB2BClient, B2BClient>();
    
    //Add B2CClient - makes business to customer payment requests
    services.AddHttpClient<IB2CClient, B2CClient>();
    
    //Add LipaNaMpesaQueryClient - Query status of a LipaNaMpesaOnline Payment request
    services.AddHttpClient<ILipaNaMpesaQueryClient, LipaNaMpesaQueryClient>();
    
    //Add TransactionReversalClient - Reverses Mpesa transactions
    services.AddHttpClient<ITransactionReversalClient, TransactionReversalClient>();
    
    //Add TransactionStatusClient - Query status of transaction requests
    services.AddHttpClient<ITransactionStatusClient, TransactionStatusClient>();  
    
     //Add AccountBalanceQueryCient - Query Mpesa balance
    services.AddHttpClient<IAccountBalanceQueryCient, AccountBalanceQueryCient>(); 
    

```
### Option 2 on how to add the services using dependency Injection:
* Option 1 above is not very clean since your startup class might get littered with too many services. To solve this, i use extention methods. The idea is to abstract these services behind one method so that we just do ```services.AddMpesaSupport()```. This makes the startup class much cleaner. To achieve this use the IserviceCollection interface available in ASP.NET Core. Here is a sample...

```c#
using Microsoft.Extensions.DependencyInjection;
using MpesaLib.Clients;
using MpesaLib.Interfaces;

namespace YourWebApp.Extensions
{
    public static class MpesaExtentions
    {
        public static void AddMpesaSupport(this IServiceCollection services)
        {
            //Add Mpesa Clients
            services.AddHttpClient<IAuthClient, AuthClient>();
            services.AddHttpClient<ILipaNaMpesaOnlineClient, LipaNaMpesaOnlineClient>();
            services.AddHttpClient<IAccountBalanceQueryClient, AccountBalanceQueryClient>();
            services.AddHttpClient<IC2BClient, C2BClient>();
            services.AddHttpClient<IB2BClient, B2BClient>();
            services.AddHttpClient<IB2CClient, B2CClient>();
            services.AddHttpClient<IC2BRegisterUrlClient, C2BRegisterUrlClient>();
            services.AddHttpClient<ILipaNaMpesaQueryClient, LipaNaMpesaQueryClient>();
            services.AddHttpClient<ITransactionStatusClient, TransactionStatusClient>();
            services.AddHttpClient<ITransactionReversalClient, TransactionReversalClient>();
        }
    }
}

```

Then in Startup.cs just add ```using YourWebApp.Extensions``` followed by ```services.AddMpesaSupport();```

* Inject the clients in the constructor of your controller or any class that makes the api calls... (in this case i only need AuthClient and LipaNaMpesaOnlineClient. I store my API Keys and secrets in a configuration file and inject them into the necessary class using ```IConfiguration``` interface.


```c#
    public class PaymentsController : Controller
    {
        private readonly IAuthClient _auth;
        private ILipaNaMpesaOnlineClient _lipaNaMpesa;
        private readonly IConfiguration _config;

        public PaymentsController(IAuthClient auth, ILipaNaMpesaOnlineClient lipaNampesa, IConfiguration configuration)
        {
            _auth = auth;
            _lipaNaMpesa = lipaNampesa;
            _config = configuration;
        }
        ...
        //Code omitted for brevity
```


* You can store your ConsumerKey and ConsumerSecret in appsettings.json as follows

```json
     "MpesaConfiguration": {
         "ConsumerKey": "[Your Mpesa ConsumerKey from daraja]",
         "ConsumerSecret": "[Your Mpesa ConsumerSecret from daraja]"
       }
```

* First generate an `accesstoken` using the AuthClient as follows

```c#
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var consumerKey = _config["MpesaConfiguration:ConsumerKey"];

            var consumerSecret = _config["MpesaConfiguration:ConsumerSecret"];
            
            
            //You can override AuthClients sandbox url when moving to production as follows
            _auth.BaseUri = "Mpesa api production Url provided by safaricom after completing GO live process"; //Only do this when moving to production, otherwise comment oout this line when in development or add an environment checker and execute the correct code
            var accesstoken = await _auth.GetToken(consumerKey,consumerSecret);

            ...
        //code omitted for brevity
```
## Note
* *Access tokens generated by the [Authclient](https://github.com/ayiemba/MpesaLib/blob/master/src/MpesaLib/Clients/AuthClient.cs) expire after an hour (this is documented in Daraja).* 

![Accesstoken Expirition period](screenshots/accesstoken.png)

* *Users of MpesaLib should ensure they handle token expriration in their code. A quick solution would be to put the semaphore in a try/catch/finally block as documented in [this question](https://stackoverflow.com/questions/49304326/refresh-token-using-static-httpclient) from stackoverflow.*

* To Send payment Request using LipaNaMpesaOnline, initialize the LipaNaMpesaOnline data transfer object as follows. The DTOs are in namespace ```using MpesaLib.Models;```

```c#
      LipaNaMpesaOnline lipaonline = new LipaNaMpesaOnline
      {
          AccountReference = "test",
          Amount = "1",
          PartyA = "254708374149",
          PartyB = "174379",
          BusinessShortCode = "174379",
          CallBackURL = "[your callback url, i wish i could help but you'll have to write your own]",
          Password = "daraga explains on how to get password",
          PhoneNumber = "254708374149",
          Timestamp = "20180716124916",//DateTime.Now.ToString("yyyyMMddHHmmss"),
          TransactionDesc = "test"
          TransactionType = "CustomerPayBillOnline" //I am using this by default, you might wanna check the other option
      };
```

* You can then make a payment request in your controller as follows...

```c#
//When Moving to production you need to overrride sandbox urls that are used by default before making api call you could probly wrap your call in an if statement that checks your environment before making api call
//To overrride sandbox url set the BaseUri property of the Mpesa Api client you are calling
_lipaNaMpesa.BaseUri = "https://api.safaricom.co.ke/mpesa/stkpush/v1/processrequest"; //only do this in production and ensure to get correct urls from safaricom after completing the GO Live process in daraja

//Otherwise proceed and make call to api
var paymentrequest = await _lipaNaMpesa.MakePayment(lipaonline, accesstoken);
```

* (Not Recommended) - If you dont want to use Dependency Injection you can just New-Up the clients and use them like this..
```c#


   var httpClient = new HttpClient(); //required, comes from System.Net.Http or Microsoft.Extensions.Http

   LipaNaMpesaOnlineClient LipaNaMpesa = new LipaNaMpesaOnlineClient(httpClient); //you have to pass in an instance of httpClient

   ...
   ...
   var paymentrequest = await LipaNaMpesa.MakePayment(lipaonline, accesstoken);
```



* Do whatever you want with the results of the request...


## A quick and dirty Way to test Using Console App:


```c#
using MpesaLib.Clients;
using MpesaLib.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        

        static void  Main(string[] args)
        {
            Console.WriteLine("Mpesa API Test ..."); 
            
            MakePaymentAsync().GetAwaiter().GetResult(); 

            Console.WriteLine("Press Any Key to Exit..");

            Console.ReadKey();

        }

        static async Task<string> MakePaymentAsync()
        {
            string ConsumerSecret = "your consumer secret from daraja";
            string ConsumerKey = "your consumer key from daraja";

            var httpClient = new HttpClient(); //this is needed by each client

           
            AuthClient Auth = new AuthClient(httpClient);   //Your have to pass in httpClient to all the MpesaLib clients.        

            string accesstoken = await Auth.GetToken(ConsumerKey, ConsumerSecret); //this will get you a token

            var LipaNaMpesaOnline = new LipaNaMpesaOnline
            {
                AccountReference = "test",
                Amount = "1",
                PartyA = "2547xxxxxxxx", //replace with your number
                PartyB = "174379",
                BusinessShortCode = "174379",

                CallBackURL = "https://use-your-own-callback-url/api/callback", //you should implement your own callback url, can be an api controller with a post method taking in a JToken

                Password = "use your own password",
                PhoneNumber = "254xxxxxxx", //same as PartyA
                Timestamp = "20180716124916", // replace with timestamp used to generate password
                TransactionDesc = "test"

            };

            LipaNaMpesaOnlineClient lipaonline = new LipaNaMpesaOnlineClient(httpClient);   //initialize the LipaNaMpesaOnlineClient()                

            var paymentdata = lipaonline.MakePayment(LipaNaMpesaOnline, accesstoken);// this will make the STK Push and if you use your personal number you should see that on your phone. If you complete the payment it will be reversed.           

            return paymentdata.ToString(); // you can return or log to console, in a real app there is plenty that you still need to do 
        }

    }
}

```
You should see the following from your phone if you configured everything propoerly...

![STK Push Screen](screenshots/stkpush.png)
