using MpesaLib.Models;
using System.Threading.Tasks;

namespace MpesaLib.Interfaces
{
    public interface IB2CClient
    {
        Task<string> PostData(BusinessToCustomer b2citem, string accesstoken);
    }
}
