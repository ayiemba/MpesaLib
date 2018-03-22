using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MpesaLib
{
    public class Token
    {
        /*Property names required like this for json, can be refactored**/
#pragma warning disable IDE1006 // Naming Styles
        public string access_token { get; set; } 
#pragma warning restore IDE1006 // Naming Styles
        public string expires_in { get; set; }
    }
}
