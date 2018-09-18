using MpesaLib.Models;
using System.Threading.Tasks;

namespace MpesaLib.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAccountBalanceQueryClient
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accbalance"></param>
        /// <param name="accesstoken"></param>
        /// <returns></returns>
        Task<string> GetBalance(AccountBalance accbalance, string accesstoken);
        
        /// <summary>
        /// 
        /// </summary>
        string BaseUri { get; set; }
    }
}
