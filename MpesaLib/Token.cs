using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MpesaLib
{
    public class Token
    {
      
        [JsonProperty]
        public string access_token { get; set; }

        [JsonProperty]
        public string expires_in { get; set; }
    }
}
