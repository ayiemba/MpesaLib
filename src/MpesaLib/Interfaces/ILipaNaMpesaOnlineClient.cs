using MpesaLib.Models;
using System.Threading.Tasks;

namespace MpesaLib.Interfaces
{
    /// <summary>
    /// Lipa Na mpesa Online Client
    /// </summary>
    public interface ILipaNaMpesaOnlineClient
    {
        /// <summary>
        /// Lipanampesa Online Cleint
        /// </summary>
        /// <param name="mpesaItem"></param>
        /// <param name="accesstoken"></param>
        /// <returns></returns>
        Task<string> MakePayment(LipaNaMpesaOnline mpesaItem, string accesstoken);
        
        /// <summary>
        /// Base Uri
        /// </summary>
        string BaseUri { get; set; }
    }
}
