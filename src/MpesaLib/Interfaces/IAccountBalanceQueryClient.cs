using MpesaLib.Models;
using System.Threading.Tasks;

namespace MpesaLib.Interfaces
{
    public interface IAccountBalanceQueryClient
    {
        Task<string> GetBalance(AccountBalance accbalance, string accesstoken);
    }
}
