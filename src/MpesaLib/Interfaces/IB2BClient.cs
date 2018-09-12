using MpesaLib.Models;
using System.Threading.Tasks;

namespace MpesaLib.Interfaces
{
    public interface IB2BClient
    {
        Task<string> PayBusiness(BusinessToBusiness b2bitem, string token);
        string BaseUri { get; set; }
    }
}
