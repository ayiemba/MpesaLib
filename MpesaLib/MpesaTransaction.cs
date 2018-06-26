using MpesaLib.Interfaces;
using MpesaLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace MpesaLib
{
    public class MpesaTransaction : IMpesaTransaction
    {
        private readonly string _consumerKey;
        private readonly string _consumerSecret;
        private static HttpClient _httpclient;

        public MpesaTransaction(HttpClient httpclient, string consumerKey, string consumerSecret)
        {
            _consumerKey = consumerKey;
            _consumerSecret = consumerSecret;
            _httpclient = httpclient;
        }

        public async Task<string> Authenticate(string consumerKey, string consumerSecret)
        {       

            var BaseAddress = new Uri("https://sandbox.safaricom.co.ke/oauth/v1/generate?grant_type=client_credentials");

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, BaseAddress);

           // var key = consumerKey + ":" + consumerSecret;
            var keyBytes = System.Text.Encoding.UTF8.GetBytes(consumerKey + ":" + consumerSecret);          

            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(keyBytes));

            HttpResponseMessage response = await _httpclient.SendAsync(request);

            var content = response.Content;

            //string jsonContent = content.ReadAsStringAsync().Result;

            var token = JsonConvert.DeserializeObject<Token>(content.ReadAsStringAsync().Result);
            return token.access_token;
        }

        //B2C
        public async Task<string> BusinessToCustomer(BusinessToCustomer b2ccontent)
        {

            var accesstoken = await Authenticate(_consumerKey, _consumerSecret);
            
            var BaseAddress = new Uri("https://sandbox.safaricom.co.ke/mpesa/b2c/v1/paymentrequest");

            _httpclient.DefaultRequestHeaders.Accept.Clear();
            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", "Bearer " + accesstoken);

            var values = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("InitiatorName", b2ccontent.InitiatorName),
                new KeyValuePair<string, string>("SecurityCredential", b2ccontent.SecurityCredential),
                new KeyValuePair<string, string>("CommandID", b2ccontent.CommandID),
                new KeyValuePair<string, string>("Amount", b2ccontent.Amount),
                new KeyValuePair<string, string>("PartyA", b2ccontent.PartyA),
                new KeyValuePair<string, string>("PartyB", b2ccontent.PartyB),
                new KeyValuePair<string, string>("Remarks", b2ccontent.Remarks),
                new KeyValuePair<string, string>("QueueTimeOutURL", b2ccontent.QueueTimeOutURL),
                new KeyValuePair<string, string>("ResultURL", b2ccontent.ResultURL),
                new KeyValuePair<string, string>("Occasion", b2ccontent.Occasion)
            };

            var request = new HttpRequestMessage(HttpMethod.Post, BaseAddress)
            {
                Content = new FormUrlEncodedContent(values)
            };

            HttpResponseMessage response = await _httpclient.SendAsync(request);

            //TODO: Remove the following line once everything works smoothly
            Console.WriteLine(accesstoken); 

            return response.Content.ReadAsStringAsync().Result;
        }

        //Lipa_Na_Mpesa_Online
        public async Task<string> LipaNaMpesaOnline(LipaNaMpesaOnline lipa)
        {
            var accesstoken = await Authenticate(_consumerKey, _consumerSecret);

            var BaseAddress = new Uri("https://sandbox.safaricom.co.ke/mpesa/stkpush/v1/processrequest");

            _httpclient.DefaultRequestHeaders.Accept.Clear();
            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpclient.DefaultRequestHeaders
                .Authorization = new AuthenticationHeaderValue("Authorization", "Bearer " + accesstoken);

            var values = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("BusinessShortCode", lipa.BusinessShortCode),
                new KeyValuePair<string, string>("Password", lipa.Password),
                new KeyValuePair<string, string>("Timestamp", lipa.Timestamp),
                new KeyValuePair<string, string>("TransactionType", lipa.TransactionType),
                new KeyValuePair<string, string>("Amount", lipa.Amount),
                new KeyValuePair<string, string>("PartyA", lipa.PartyA),
                new KeyValuePair<string, string>("PartyB", lipa.PartyB),
                new KeyValuePair<string, string>("PhoneNumber", lipa.PhoneNumber),
                new KeyValuePair<string, string>("CallBackURL", lipa.CallBackURL),
                new KeyValuePair<string, string>("AccountReference", lipa.AccountReference),
                new KeyValuePair<string, string>("TransactionDesc", lipa.TransactionDesc)
            };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, BaseAddress)
            {
                Content = new FormUrlEncodedContent(values)
            };


            HttpResponseMessage response = await _httpclient.SendAsync(request);

            //TODO: Remove the following line once everything works smoothly
            Console.WriteLine(accesstoken);

            return response.Content.ReadAsStringAsync().Result;

        }

        

        public async Task<string> BusinessToBusiness(BusinessToBusiness b2b)
        {
            string tokendata = await Authenticate(_consumerKey, _consumerSecret);

            var accesstoken = JsonConvert.DeserializeObject<Token>(tokendata);


            throw new NotImplementedException();
        }     

        public Task<string> CustomerToBusinessRegister(CustomerToBusinessRegister c2bregister)
        {
            throw new NotImplementedException();
        }

        public Task<string> CustomerToBusinessSimulate(CustomerToBusinessSimulate c2bsimulate)
        {
            throw new NotImplementedException();
        }

        public Task<string> LipaNaMpesaQuery(LipaNaMpesaQuery query)
        {
            throw new NotImplementedException();
        }

        public Task<string> Reversal(Reversal reversal)
        {
            throw new NotImplementedException();
        }

        public Task<string> TransactionStatus(TransactionStatus status)
        {
            throw new NotImplementedException();
        }

        public Task<string> AccountBalance(AccountBalance balance)
        {
            throw new NotImplementedException();
        }
    }
}
