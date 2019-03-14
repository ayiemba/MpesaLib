using System;
using System.Collections.Generic;
using System.Text;

namespace MpesaLib.Helpers.Exceptions
{
    /// <summary>
    /// Mpesa Api Exceptions Helper class
    /// </summary>
    public class MpesaApiException : Exception
    {
        /// <summary>
        /// Http Status code
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Content from  response body
        /// </summary>
        public string Content { get; set; }
    }
}
