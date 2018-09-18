using MpesaLib.Models;
using System.Threading.Tasks;

namespace MpesaLib.Interfaces
{
    /// <summary>
    /// LipaNaMpesaOnlineQuery client
    /// </summary>
    public interface ILipaNaMpesaQueryClient
    {
        /// <summary>
        /// Makes LipaNaMpesaOnline Query
        /// </summary>
        /// <param name="mpesaQuery"></param>
        /// <param name="accesstoken"></param>
        /// <returns></returns>
        Task<string> MakeMpesaQuery(LipaNaMpesaQuery mpesaQuery, string accesstoken);
       
        /// <summary>
        /// Base Uri
        /// </summary>
        string BaseUri { get; set; }
    }
}
