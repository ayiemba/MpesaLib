using MpesaLib.Models;
using System.Threading.Tasks;

namespace MpesaLib.Interfaces
{
    /// <summary>
    /// B2B Client
    /// </summary>
    public interface IB2BClient
    {
        /// <summary>
        /// B2B Client
        /// </summary>
        /// <param name="b2bitem"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<string> PayBusiness(BusinessToBusiness b2bitem, string token);
        
        /// <summary>
        /// 
        /// </summary>
        string BaseUri { get; set; }
    }
}
