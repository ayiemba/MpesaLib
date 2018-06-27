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
            

            var keyBytes = System.Text.Encoding.UTF8.GetBytes(consumerKey + ":" + consumerSecret);
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(keyBytes));

            //_httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(keyBytes));

            HttpResponseMessage response = await _httpclient.SendAsync(request);

            var content = response.Content;

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
            //_httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", "Bearer " + accesstoken);

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

            request.Headers.Authorization = new AuthenticationHeaderValue("Authorization", "Bearer " + accesstoken);

            HttpResponseMessage response = await _httpclient.SendAsync(request);

            //TODO: Remove the following line once everything works smoothly, 
            // this is just to show that an access token is actually received
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
           // _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", "Bearer " + accesstoken);

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

            request.Headers.Authorization = new AuthenticationHeaderValue("Authorization", "Bearer " + accesstoken);


            HttpResponseMessage response = await _httpclient.SendAsync(request);

            //TODO: Remove the following line once everything works smoothly
            Console.WriteLine(accesstoken);

            return response.Content.ReadAsStringAsync().Result;

        }

        

        public async Task<string> BusinessToBusiness(BusinessToBusiness b2b)
        {
            var accesstoken = await Authenticate(_consumerKey, _consumerSecret);

            var BaseAddress = new Uri("https://sandbox.safaricom.co.ke/mpesa/b2b/v1/paymentrequest");
            _httpclient.DefaultRequestHeaders.Accept.Clear();
            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //_httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", "Bearer " + accesstoken);

            var values = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Initiator", b2b.Initiator),
                new KeyValuePair<string, string>("SecurityCredential", b2b.SecurityCredential),
                new KeyValuePair<string, string>("CommandID", b2b.CommandID),
                new KeyValuePair<string, string>("SenderIdentifierType", b2b.SenderIdentifierType),
                new KeyValuePair<string, string>("RecieverIdentifierType", b2b.RecieverIdentifierType),
                new KeyValuePair<string, string>("Amount", b2b.Amount),
                new KeyValuePair<string, string>("PartyA", b2b.PartyA),
                new KeyValuePair<string, string>("PartyB", b2b.PartyB),
                new KeyValuePair<string, string>("AccountReference", b2b.AccountReference),
                new KeyValuePair<string, string>("Remarks", b2b.Remarks),
                new KeyValuePair<string, string>("QueueTimeOutURL", b2b.QueueTimeOutURL),
                new KeyValuePair<string, string>("ResultURL", b2b.ResultURL),
            };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, BaseAddress)
            {
                Content = new FormUrlEncodedContent(values)
            };

            request.Headers.Authorization = new AuthenticationHeaderValue("Authorization", "Bearer " + accesstoken);


            HttpResponseMessage response = await _httpclient.SendAsync(request);

            //TODO: Remove the following line once everything works smoothly
            Console.WriteLine(accesstoken);

            return response.Content.ReadAsStringAsync().Result;
        }     

        public async Task<string> CustomerToBusinessRegister(CustomerToBusinessRegister c2bregister)
        {
            var accesstoken = await Authenticate(_consumerKey, _consumerSecret);

            var BaseAddress = new Uri("https://sandbox.safaricom.co.ke/mpesa/c2b/v1/registerurl");
            _httpclient.DefaultRequestHeaders.Accept.Clear();
            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //_httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", "Bearer " + accesstoken);

            var values = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("ShortCode", c2bregister.ShortCode),
                new KeyValuePair<string, string>("ResponseType", c2bregister.ResponseType),
                new KeyValuePair<string, string>("ConfirmationURL", c2bregister.ConfirmationURL),
                new KeyValuePair<string, string>("ValidationURL", c2bregister.ValidationURL)
               
            };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, BaseAddress)
            {
                Content = new FormUrlEncodedContent(values)
            };

            request.Headers.Authorization = new AuthenticationHeaderValue("Authorization", "Bearer " + accesstoken);


            HttpResponseMessage response = await _httpclient.SendAsync(request);

            //TODO: Remove the following line once everything works smoothly
            Console.WriteLine(accesstoken);

            return response.Content.ReadAsStringAsync().Result;
        }

        public async Task<string> CustomerToBusinessSimulate(CustomerToBusinessSimulate c2bsimulate)
        {
            var accesstoken = await Authenticate(_consumerKey, _consumerSecret);

            var BaseAddress = new Uri("https://sandbox.safaricom.co.ke/mpesa/c2b/v1/simulate");

            _httpclient.DefaultRequestHeaders.Accept.Clear();
            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //_httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", "Bearer " + accesstoken);

            var values = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("ShortCode", c2bsimulate.ShortCode),
                new KeyValuePair<string, string>("CommandID", c2bsimulate.CommandID),
                new KeyValuePair<string, string>("Amount", c2bsimulate.Amount),
                new KeyValuePair<string, string>("Msisdn", c2bsimulate.Msisdn),
                new KeyValuePair<string, string>("BillRefNumber", c2bsimulate.BillRefNumber)
                
            };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, BaseAddress)
            {
                Content = new FormUrlEncodedContent(values)
            };

            request.Headers.Authorization = new AuthenticationHeaderValue("Authorization", "Bearer " + accesstoken);


            HttpResponseMessage response = await _httpclient.SendAsync(request);

            //TODO: Remove the following line once everything works smoothly
            Console.WriteLine(accesstoken);

            return response.Content.ReadAsStringAsync().Result;
        }

        public async Task<string> LipaNaMpesaQuery(LipaNaMpesaQuery query)
        {
            var accesstoken = await Authenticate(_consumerKey, _consumerSecret);

            var BaseAddress = new Uri("https://sandbox.safaricom.co.ke/mpesa/stkpushquery/v1/query");

            _httpclient.DefaultRequestHeaders.Accept.Clear();
            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //_httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", "Bearer " + accesstoken);

            var values = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("BusinessShortCode", query.BusinessShortCode),
                new KeyValuePair<string, string>("Password", query.Password),
                new KeyValuePair<string, string>("Timestamp", query.Timestamp),
                new KeyValuePair<string, string>("CheckoutRequestID", query.CheckoutRequestID)
                
            };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, BaseAddress)
            {
                Content = new FormUrlEncodedContent(values)
            };

            request.Headers.Authorization = new AuthenticationHeaderValue("Authorization", "Bearer " + accesstoken);


            HttpResponseMessage response = await _httpclient.SendAsync(request);

            //TODO: Remove the following line once everything works smoothly
            Console.WriteLine(accesstoken);

            return response.Content.ReadAsStringAsync().Result;
        }

        public async Task<string> Reversal(Reversal reversal)
        {
            var accesstoken = await Authenticate(_consumerKey, _consumerSecret);

            var BaseAddress = new Uri("https://sandbox.safaricom.co.ke/mpesa/reversal/v1/request");

            _httpclient.DefaultRequestHeaders.Accept.Clear();
            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //_httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", "Bearer " + accesstoken);

            var values = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Initiator", reversal.Initiator),
                new KeyValuePair<string, string>("SecurityCredential", reversal.SecurityCredential),
                new KeyValuePair<string, string>("CommandID", reversal.CommandID),
                new KeyValuePair<string, string>("TransactionID", reversal.TransactionID),
                new KeyValuePair<string, string>("Amount", reversal.Amount),
                new KeyValuePair<string, string>("ReceiverParty", reversal.ReceiverParty),
                new KeyValuePair<string, string>("RecieverIdentifierType", reversal.RecieverIdentifierType),
                new KeyValuePair<string, string>("ResultURL", reversal.ResultURL),
                new KeyValuePair<string, string>("QueueTimeOutURL", reversal.QueueTimeOutURL),
                new KeyValuePair<string, string>("Remarks", reversal.Remarks),
                new KeyValuePair<string, string>("Occasion", reversal.Occasion)
                
            };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, BaseAddress)
            {
                Content = new FormUrlEncodedContent(values)
            };

            request.Headers.Authorization = new AuthenticationHeaderValue("Authorization", "Bearer " + accesstoken);


            HttpResponseMessage response = await _httpclient.SendAsync(request);

            //TODO: Remove the following line once everything works smoothly
            Console.WriteLine(accesstoken);

            return response.Content.ReadAsStringAsync().Result;
        }

        public async Task<string> TransactionStatus(TransactionStatus status)
        {
            var accesstoken = await Authenticate(_consumerKey, _consumerSecret);

            var BaseAddress = new Uri("https://sandbox.safaricom.co.ke/mpesa/transactionstatus/v1/query");

            _httpclient.DefaultRequestHeaders.Accept.Clear();
            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //_httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", "Bearer " + accesstoken);

            var values = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Initiator", status.Initiator),
                new KeyValuePair<string, string>("SecurityCredential", status.SecurityCredential),
                new KeyValuePair<string, string>("CommandID", status.CommandID),
                new KeyValuePair<string, string>("TransactionID", status.TransactionID),
                new KeyValuePair<string, string>("PartyA", status.PartyA),
                new KeyValuePair<string, string>("IdentifierType", status.IdentifierType),
                new KeyValuePair<string, string>("ResultURL", status.ResultURL),
                new KeyValuePair<string, string>("QueueTimeOutURL", status.QueueTimeOutURL),
                new KeyValuePair<string, string>("Remarks", status.Remarks),
                new KeyValuePair<string, string>("Occasion", status.Occasion)
                
            };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, BaseAddress)
            {
                Content = new FormUrlEncodedContent(values)
            };

            request.Headers.Authorization = new AuthenticationHeaderValue("Authorization", "Bearer " + accesstoken);


            HttpResponseMessage response = await _httpclient.SendAsync(request);

            //TODO: Remove the following line once everything works smoothly
            Console.WriteLine(accesstoken);

            return response.Content.ReadAsStringAsync().Result;
        }

        public async Task<string> AccountBalance(AccountBalance balance)
        {
            var accesstoken = await Authenticate(_consumerKey, _consumerSecret);

            var BaseAddress = new Uri("https://sandbox.safaricom.co.ke/mpesa/accountbalance/v1/query");

            _httpclient.DefaultRequestHeaders.Accept.Clear();
            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //_httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", "Bearer " + accesstoken);

            var values = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Initiator", balance.Initiator),
                new KeyValuePair<string, string>("SecurityCredential", balance.SecurityCredential),
                new KeyValuePair<string, string>("CommandID", balance.CommandID),
                new KeyValuePair<string, string>("PartyA", balance.PartyA),
                new KeyValuePair<string, string>("IdentifierType", balance.IdentifierType),
                new KeyValuePair<string, string>("Remarks", balance.Remarks),
                new KeyValuePair<string, string>("QueueTimeOutURL", balance.QueueTimeOutURL),
                new KeyValuePair<string, string>("ResultURL", balance.ResultURL),
            };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, BaseAddress)
            {
                Content = new FormUrlEncodedContent(values)
            };

            request.Headers.Authorization = new AuthenticationHeaderValue("Authorization", "Bearer " + accesstoken);


            HttpResponseMessage response = await _httpclient.SendAsync(request);

            //TODO: Remove the following line once everything works smoothly
            Console.WriteLine(accesstoken);

            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
