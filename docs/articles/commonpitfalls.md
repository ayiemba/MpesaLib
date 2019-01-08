
## Getting Security Credential for B2B, B2C, Reversal, Transaction Status and Account Balance APIs
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

## Async vs Sync Tips
** You can use ```_mpesaClient.GetAuthToken(*args).GetAwaiter().GetResult();``` instead of ```await _mpesaClient.GetAuthToken(*args);```  when for one reason or another you can't use asynchronous methods.

## 14. Magic Strings
From version 3.X.X of MpesaLib you can avoid having to put magic strings in your method calls by taking advantage of the 
```c# MpesaLib.RequestEndPoint``` class. The RequestEndPoint Class defines all the string for you so you don't have to worry about getting an endpoint wrong. For example now you do the following..
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
