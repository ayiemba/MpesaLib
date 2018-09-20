using MpesaLib.Models;
using System.Threading.Tasks;

namespace MpesaLib.Interfaces
{
    /// <summary>
    /// Transaction Reversal Client
    /// </summary>
    public interface ITransactionReversalClient
    {
        /// <summary>
        /// Reverse transaction
        /// </summary>
        /// <param name="reversal"></param>
        /// <param name="accesstoken"></param>
        /// <returns></returns>
        Task<string> ReverseTransaction(Reversal reversal, string accesstoken);

        
        /// <summary>
        /// Base Uri
        /// </summary>

        string BaseUri { get; set; }
    }
}
