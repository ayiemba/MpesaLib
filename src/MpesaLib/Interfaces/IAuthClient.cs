using System.Threading.Tasks;

namespace MpesaLib.Interfaces
{
    /// <summary>
    /// Get Auth accesstoken
    /// </summary>
    public interface IAuthClient
    {
        /// <summary>
        /// Get accesstoken
        /// </summary>
        /// <param name="consumerKey"></param>
        /// <param name="consumerSecret"></param>
        /// <returns></returns>
        Task<string> GetToken(string consumerKey, string consumerSecret);

        /// <summary>
        /// Base Uri
        /// </summary>

        string BaseUri { get; set; }
    }
}
