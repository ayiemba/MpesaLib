
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
