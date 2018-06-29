using MpesaLib.Models;
using System.Threading.Tasks;

namespace MpesaLib.Interfaces
{
    public interface IAccountBalanceQueryClient
    {
        Task<string> GetData(AccountBalance accbalance, string accesstoken);
    }
}
