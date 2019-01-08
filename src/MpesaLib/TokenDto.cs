using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MpesaLib
{
    /// <summary>
    /// Accesstoken data transfer object
    /// </summary>
    public class TokenDto
    {
        /// <summary>
        /// Access token to access other APIs
        /// </summary>
        [JsonProperty("access_token")]
        public string access_token { get; set; }

        /// <summary>
        /// time token expires
        /// </summary>
        [JsonProperty("expires_in")]
        public string expires_in { get; set; }
    }
}
