using MpesaLib.Clients;
using MpesaLib.Models;
using System.Threading.Tasks;

namespace MpesaLib.Interfaces
{
    /// <summary>
    /// C2B Client
    /// </summary>
    public interface IC2BClient
    {
        /// <summary>
        /// C2B Client
        /// </summary>
        /// <param name="c2bsimulate"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<string> MakeC2BPayment(CustomerToBusinessSimulate c2bsimulate, string token);
        
        /// <summary>
        /// Base Uri
        /// </summary>
        string BaseUri { get; set; }
    }
}
