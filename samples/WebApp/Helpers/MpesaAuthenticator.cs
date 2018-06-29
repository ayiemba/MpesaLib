using MpesaLib.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Helpers
{
    public class MpesaAuthenticator
    {
        private AuthClient _authclient;
        public string _consumerKey { get; }
        public string _consumerSecret { get; }

        public MpesaAuthenticator(AuthClient auth, string consumerKey, string consumerSecret)
        {
            _authclient = auth;
            _consumerKey = consumerKey;
            _consumerSecret = consumerSecret;
        }      

        public async Task<string> GetToken()
        {
            var accesstoken = await _authclient.GetData(_consumerKey, _consumerSecret);
            return accesstoken;
            
        }
    }
}
