using MpesaLib.Models;
using System.Threading.Tasks;

namespace MpesaLib.Interfaces
{
    public interface ILipaNaMpesaQueryClient
    {
        Task<string> GetData(LipaNaMpesaQuery mpesaQuery, string accesstoken);
    }
}
