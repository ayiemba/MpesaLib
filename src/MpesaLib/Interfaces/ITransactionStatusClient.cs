using MpesaLib.Models;
using System.Threading.Tasks;
using System.Transactions;

namespace MpesaLib.Interfaces
{
    public interface ITransactionStatusClient
    {
        Task<string> GetTransactionStatus(MpesaTransactionStatus transactionStatus, string accesstoken);
    }
}
