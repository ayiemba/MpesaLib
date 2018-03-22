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

        public static async Task<string> Authenticate(string consumerKey, string consumerSecret)
        {
            HttpClient httpclient = new HttpClient
            {
                BaseAddress = new Uri("https://sandbox.safaricom.co.ke/oauth/v1/generate?grant_type=client_credentials")
            };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get,httpclient.BaseAddress);

            //consumerKey = "HzROja3XIZJiCIfzsMj59xyL2GR2S52C";
            //consumerSecret = "c7cB7AU3c0uyYxxd";
            var key = consumerKey + ":" + consumerSecret;
            var keyBytes = System.Text.Encoding.UTF8.GetBytes(key);          

            httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(keyBytes));

            HttpResponseMessage response = await httpclient.SendAsync(request);

            var content = response.Content;

            string jsonContent = content.ReadAsStringAsync().Result;
            return jsonContent;
        }

        //B2C
        public async Task<string> BusinessToCustomer(string InitiatorName, string SecurityCredential,
            string CommandID, string Amount, string PartyA, string PartyB, string Remarks,
            string QueueTimeOutURL, string ResultURL, string Occasion)
        {
            B2C b2ccontent = new B2C();

            //string tokendata = await Authenticate();         
            //var tokenresult = JsonConvert.DeserializeObject<Token>(tokendata);
            string accesstoken = "Fv70J7eOJeGxHJIYM9te3V9WV9vt";

            HttpClient httpclient = new HttpClient
            {
                BaseAddress = new Uri("https://sandbox.safaricom.co.ke/mpesa/b2c/v1/paymentrequest")
            };

            httpclient.DefaultRequestHeaders.Accept.Clear();
            httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpclient.DefaultRequestHeaders
                .Authorization = new AuthenticationHeaderValue("Authorization", "Bearer " + accesstoken);

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

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, httpclient.BaseAddress)
            {
                Content = new FormUrlEncodedContent(values)
            };


            HttpResponseMessage response = await httpclient.SendAsync(request);

            var content = response.Content;

            string jsonContent = content.ReadAsStringAsync().Result;
            Console.WriteLine(jsonContent);
            return jsonContent;
        }

        //Lipa_Na_Mpesa_Online
        public async Task<string> LipaNaMpesaOnline(string BusinessShortCode, string Password, string Timestamp,
            string TransactionType, string Amount, string PartyA, string PartyB, string PhoneNumber,
            string CallBackURL, string AccountReference, string TransactionDesc)
        {
            LipaNaMpesaOnline lipa = new LipaNaMpesaOnline();
            string accesstoken = "Fv70J7eOJeGxHJIYM9te3V9WV9vt";
            lipa.Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString();

            HttpClient httpclient = new HttpClient
            {
                BaseAddress = new Uri("https://sandbox.safaricom.co.ke/mpesa/stkpush/v1/processrequest")
            };

            httpclient.DefaultRequestHeaders.Accept.Clear();
            httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpclient.DefaultRequestHeaders
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

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, httpclient.BaseAddress)
            {
                Content = new FormUrlEncodedContent(values)
            };


            HttpResponseMessage response = await httpclient.SendAsync(request);

            var content = response.Content;

            string jsonContent = content.ReadAsStringAsync().Result;
            Console.WriteLine(jsonContent);
            return jsonContent;

        }

        public Task<string> AccountBalance(string Initiator, string SecurityCredential, string CommandID, 
            string Amount, string PartyA, string IdentifierType, string Remarks, string QueueTimeOutURL, string ResultURL)
        {
            throw new NotImplementedException();
        }

        public Task<string> BusinessToBusiness(string Initiator, string SecurityCredential, 
            string CommandID, string SenderIdentifierType, string RecieverIdentifierType, 
            string Amount, string PartyA, string PartyB, string AccountReference, string Remarks, 
            string QueueTimeOutURL, string ResultURL)
        {
            throw new NotImplementedException();
        }     

        public Task<string> CustomerToBusinessRegister(string ShortCode, string ResponseType, 
            string ConfirmationURL, string ValidationURL)
        {
            throw new NotImplementedException();
        }

        public Task<string> CustomerToBusinessSimulate(string ShortCode, string CommandID,
            string Amount, string Msisdn, string BillRefNumber)
        {
            throw new NotImplementedException();
        }

        public Task<string> LipaNaMpesaQuery(string BusinessShortCode, string Password, 
            string Timestamp, string CheckoutRequestID)
        {
            throw new NotImplementedException();
        }

        public Task<string> Reversal(string InitiatorName, string SecurityCredential, string CommandID, 
            string TransactionID, string Amount, string ReceiverParty, string RecieverIdentifierType, 
            string Remarks, string QueueTimeOutURL, string ResultURL, string Occasion)
        {
            throw new NotImplementedException();
        }

        public Task<string> TransactionStatus(string Initiator, string SecurityCredential, string CommandID, 
            string TransactionID, string PartyA, string IdentifierType, string Remarks, 
            string QueueTimeOutURL, string ResultURL, string Occasion)
        {
            throw new NotImplementedException();
        }    


    }
}
