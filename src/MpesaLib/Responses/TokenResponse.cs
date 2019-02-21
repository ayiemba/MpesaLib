using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public string AccessToken { get; set; }

        /// <summary>
        /// time token expires
        /// </summary>
        [JsonProperty("expires_in")]
        public string ExpiresIn { get; set; }
    }
}
