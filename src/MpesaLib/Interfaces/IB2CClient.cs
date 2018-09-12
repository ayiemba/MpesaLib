using MpesaLib.Models;
using System.Threading.Tasks;

namespace MpesaLib.Interfaces
{
    public interface IB2CClient
    {
        Task<string> PayCustomer(BusinessToCustomer b2citem, string accesstoken);
        string BaseUri { get; set; }
    }
}
