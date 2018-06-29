using MpesaLib.Models;
using System.Threading.Tasks;

namespace MpesaLib.Interfaces
{
    public interface IB2BClient
    {
        Task<string> PostData(BusinessToBusiness b2bitem, string token);
    }
}
