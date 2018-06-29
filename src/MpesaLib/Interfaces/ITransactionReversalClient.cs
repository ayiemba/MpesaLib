using MpesaLib.Models;
using System.Threading.Tasks;

namespace MpesaLib.Interfaces
{
    public interface ITransactionReversalClient
    {
        Task<string> ReverseTransaction(Reversal reversal, string accesstoken);
    }
}
