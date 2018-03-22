
using System.Threading.Tasks;

namespace MpesaLib
{
    public interface IMpesaTransaction
    {
        Task<string> BusinessToCustomer(string InitiatorName, string SecurityCredential, 
            string CommandID, string Amount, string PartyA, string PartyB, string Remarks, 
            string QueueTimeOutURL, string ResultURL, string Occasion);

        Task<string> LipaNaMpesaOnline(string BusinessShortCode, string Password, string Timestamp, 
            string TransactionType, string Amount, string PartyA, string PartyB, string PhoneNumber, 
            string CallBackURL, string AccountReference, string TransactionDesc);

        Task<string> BusinessToBusiness(string Initiator, string SecurityCredential,
            string CommandID,string SenderIdentifierType,string RecieverIdentifierType, string Amount, string PartyA, 
            string PartyB, string AccountReference, string Remarks, string QueueTimeOutURL, string ResultURL);

        Task<string> CustomerToBusinessRegister(string ShortCode, string ResponseType,
            string ConfirmationURL, string ValidationURL);

        Task<string> CustomerToBusinessSimulate(string ShortCode,
            string CommandID, string Amount, string Msisdn, string BillRefNumber);

        Task<string> LipaNaMpesaQuery(string BusinessShortCode, string Password,
            string Timestamp, string CheckoutRequestID);

        Task<string> Reversal(string InitiatorName, string SecurityCredential,
            string CommandID, string TransactionID, string Amount, string ReceiverParty, string RecieverIdentifierType, string Remarks,
            string QueueTimeOutURL, string ResultURL, string Occasion);

        Task<string> TransactionStatus(string Initiator, string SecurityCredential,
            string CommandID, string TransactionID, string PartyA, string IdentifierType, string Remarks,
            string QueueTimeOutURL, string ResultURL, string Occasion);

        Task<string> AccountBalance(string Initiator, string SecurityCredential,
            string CommandID, string Amount, string PartyA, string IdentifierType, string Remarks,
            string QueueTimeOutURL, string ResultURL);

    }
}