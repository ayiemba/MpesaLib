using Newtonsoft.Json;

namespace MpesaLib.Responses
{
    /// <summary>
    /// Accesstoken data transfer object
    /// </summary>
    public class TokenResponse
    {
        /// <summary>
        /// Access token to access other APIs
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; private set; }

        /// <summary>
        /// time token expires
        /// </summary>
        [JsonProperty("expires_in")]
        public string ExpiresIn { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="access_token">Access token required to access other Mpesa API endpoints</param>
        /// <param name="expires_in">time in seconds after which the token expires</param>
        public TokenResponse(string access_token, string expires_in)
        {
            AccessToken = access_token;
            ExpiresIn = expires_in;
        }
    }
}
