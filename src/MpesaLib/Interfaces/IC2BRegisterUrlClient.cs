using MpesaLib.Models;
using System.Threading.Tasks;

namespace MpesaLib.Interfaces
{
    /// <summary>
    /// C2B Register Url client
    /// </summary>
    public interface IC2BRegisterUrlClient
    {
        /// <summary>
        /// Register Url client
        /// </summary>
        /// <param name="c2bregisterItem"></param>
        /// <param name="accesstoken"></param>
        /// <returns></returns>
        Task<string> RegisterUrl(CustomerToBusinessRegister c2bregisterItem, string accesstoken);
        
        /// <summary>
        /// Base Uri
        /// </summary>
        string BaseUri { get; set; }
    }
}
