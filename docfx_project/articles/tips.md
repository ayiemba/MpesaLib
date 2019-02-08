
## Getting Security Credential for B2B, B2C, Reversal, Transaction Status and Account Balance APIs
The Security Credential helper class is in MpesaLib.Helpers namespace.

This class helps you generate the required credential to be used to authorize the above mentioned APIs.

```c#
using MpesaLib.Helpers; // Add this to your class

//get path of Mpesa public certificate. There are different certs for development and for production, ensure to use the correct one)
string certificate = @"C:\Dev\MpesaLibSamples\WebApplication1\Certificate\prod.cer";
 
 //generate security credential as follows...
var SecutityCredential = Credentials.EncryptPassword(certificate, "Initiator Password");

```
