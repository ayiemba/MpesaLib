using MpesaLib.Models;
using System.Threading.Tasks;

namespace MpesaLib.Interfaces
{
    public interface IC2BRegisterUrlClient
    {
        Task<string> RegisterUrl(CustomerToBusinessRegister c2bregisterItem, string accesstoken);
    }
}
