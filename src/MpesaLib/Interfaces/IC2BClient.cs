using MpesaLib.Clients;
using MpesaLib.Models;
using System.Threading.Tasks;

namespace MpesaLib.Interfaces
{
    public interface IC2BClient
    {
        Task<string> MakeC2BPayment(CustomerToBusinessSimulate c2bsimulate, string token);
    }
}
