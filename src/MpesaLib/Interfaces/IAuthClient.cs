using System.Threading.Tasks;

namespace MpesaLib.Interfaces
{
    public interface IAuthClient
    {
        Task<string> GetData(string consumerKey, string consumerSecret);
    }
}
