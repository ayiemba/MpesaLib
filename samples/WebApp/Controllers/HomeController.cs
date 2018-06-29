using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MpesaLib.Clients;
using MpesaLib.Models;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private AuthClient _auth;
        private C2BSimulateClient _c2bsim;
        private LipaNaMpesaOnlineClient _lipaNaMpesaOnline;
        private IConfiguration _configuration;

        public HomeController(AuthClient auth, C2BSimulateClient c2bsimulate, LipaNaMpesaOnlineClient lipaOnline, IConfiguration configuration)
        {
            _auth = auth;
            _c2bsim = c2bsimulate;
            _lipaNaMpesaOnline = lipaOnline;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> About()
        {
            var consumerKey = _configuration["MpesaConfiguration:ConsumerKey"];
            
            var consumerSecret = _configuration["MpesaConfiguration:ConsumerSecret"];

            var c2b_accesstoken = await _auth.GetData(consumerKey, consumerSecret);
            var lipaOnlineAccesstoken = await _auth.GetData(consumerKey, consumerSecret);

            var items = new Items();

            var c2bdata = await _c2bsim.PostData(items.c2b, c2b_accesstoken);

            var lipaData = await _lipaNaMpesaOnline.MakePayment(items.lipaOnline, lipaOnlineAccesstoken);

            ViewData["Message"] = c2bdata;

            ViewData["Message2"] = lipaData;

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
