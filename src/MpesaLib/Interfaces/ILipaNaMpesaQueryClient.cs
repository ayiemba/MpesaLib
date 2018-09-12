using MpesaLib.Models;
using System.Threading.Tasks;

namespace MpesaLib.Interfaces
{
    public interface ILipaNaMpesaQueryClient
    {
        Task<string> MakeMpesaQuery(LipaNaMpesaQuery mpesaQuery, string accesstoken);
        string BaseUri { get; set; }
    }
}
