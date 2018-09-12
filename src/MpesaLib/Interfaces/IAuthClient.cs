using System.Threading.Tasks;

namespace MpesaLib.Interfaces
{
    public interface IAuthClient
    {
        Task<string> GetToken(string consumerKey, string consumerSecret);
        string BaseUri { get; set; }
    }
}
