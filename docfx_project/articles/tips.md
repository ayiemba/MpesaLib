
## Getting Security Credential for B2B, B2C, Reversal, Transaction Status and Account Balance APIs
The Security Credential helper class is in MpesaLib.Helpers namespace.
The signature is as follows.. 

```c# 
var SecutityCredential = Credentials.EncryptPassword(pathToSafaricomPublicCertificate, YourInitiatorpassword)
```
e.g
```c#
using MpesaLib.Helpers; // Add this to your class

//get path of Mpesa public certificate
 string certificate = @"C:\Dev\MpesaIntegration\MpesaLibSamples\WebApplication1\Certificate\prod.cer";
 
 //generate security credential as follows...
 var SecutityCredential = Credentials.EncryptPassword(certificate, "971796");

```

## Async vs Sync Tips
** MpesaLib Clients provide two methods, one asynchronous and one synchronous. For Example the ```ReverseMpesaTransactionAsync()``` Vs ```ReverseMpesaTransaction()```.

Use Asynchronous method in a method/controller that has the async keyword.
Use Syncronous method in a method/controller that does not have the async keyword.

*The Synchronous method actually uses a ```.GetAwaiter().GetResutl()``` internally on the HttpClient request so it is not entirely synchronous*

## Dealing With Magic Strings
From version 3.X.X of MpesaLib you can avoid having to put magic strings in your method calls by taking advantage of the 
```MpesaLib.RequestEndPoint``` class. The RequestEndPoint Class defines all the strings for you so you don't have to worry about getting an endpoints wrong. For example you can do the following..
```c#
await _mpesaClient.ReverseMpesaTransactionAsync(TransactionReversalObject, accesstoken, RequestEndPoint.ReverseMpesaTransaction);
```
instead of...

```c#
await _mpesaClient.ReverseMpesaTransactionAsync(TransactionReversalObject, accesstoken, "mpesa/reversal/v1/request");
```

The same when setting BaseAdress...
For Sandbox:
```c#
services.AddHttpClient<IMpesaClient, MpesaClient>(opts=>opts.BaseAddress = RequestEndPoint.SandboxBaseAddress);
```
For Live API:
```c#
services.AddHttpClient<IMpesaClient, MpesaClient>(opts=>opts.BaseAddress = RequestEndPoint.LiveBaseAddress);
```
