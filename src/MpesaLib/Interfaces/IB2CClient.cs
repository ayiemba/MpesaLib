using MpesaLib.Models;
using System.Threading.Tasks;

namespace MpesaLib.Interfaces
{
    /// <summary>
    /// B2C Client
    /// </summary>
    public interface IB2CClient
    {
        /// <summary>
        /// B2C Client
        /// </summary>
        /// <param name="b2citem"></param>
        /// <param name="accesstoken"></param>
        /// <returns></returns>
        Task<string> PayCustomer(BusinessToCustomer b2citem, string accesstoken);


        /// <summary>
        /// Base uri
        /// </summary>

        string BaseUri { get; set; }
    }
}
