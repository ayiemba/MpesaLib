using MpesaLib.Models;
using System.Threading.Tasks;

namespace MpesaLib.Interfaces
{
    public interface ILipaNaMpesaOnlineClient
    {
        Task<string> MakePayment(LipaNaMpesaOnline mpesaItem, string accesstoken);
    }
}
