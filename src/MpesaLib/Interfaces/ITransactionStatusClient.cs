using MpesaLib.Models;
using System.Threading.Tasks;

namespace MpesaLib.Interfaces
{
    /// <summary>
    /// Query Transaction status
    /// </summary>
    public interface ITransactionStatusClient
    {
        /// <summary>
        /// Query transaction status
        /// </summary>
        /// <param name="transactionStatus"></param>
        /// <param name="accesstoken"></param>
        /// <returns></returns>
        Task<string> GetTransactionStatus(MpesaTransactionStatus transactionStatus, string accesstoken);


        /// <summary>
        /// Base Uri
        /// </summary>

        string BaseUri { get; set; }
    }
}
