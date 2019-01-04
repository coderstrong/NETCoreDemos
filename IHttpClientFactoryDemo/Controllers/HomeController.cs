using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IHttpClientFactoryDemo.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;

namespace IHttpClientFactoryDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _clientHttpFactory;

        public HomeController(IHttpClientFactory clientHttpFactory){
            _clientHttpFactory = clientHttpFactory;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> About()
        {
            HttpClient client  = _clientHttpFactory.CreateClient("callgoogle");
            var res = await client.GetStringAsync("search?q=IHttpClientFactory");
            
            ViewData["Message"] = res;
            return View();
        }

        [Authorize]
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
