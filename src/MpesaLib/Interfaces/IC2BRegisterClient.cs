using MpesaLib.Models;
using System.Threading.Tasks;

namespace MpesaLib.Interfaces
{
    public interface IC2BRegisterClient
    {
        Task<string> GetData(CustomerToBusinessRegister c2bregisterItem, string accesstoken);
    }
}
