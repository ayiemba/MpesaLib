using MpesaLib.Clients;
using MpesaLib.Models;
using System.Threading.Tasks;

namespace MpesaLib.Interfaces
{
    public interface IC2BSimulateClient
    {
        Task<string> PostData(CustomerToBusinessSimulate c2bsimulate, string token);
    }
}
