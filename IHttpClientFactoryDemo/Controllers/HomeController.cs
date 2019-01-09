using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IHttpClientFactoryDemo.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace IHttpClientFactoryDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _clientHttpFactory;

        public ILogger<HomeController> Logger { get; }

        public HomeController(IHttpClientFactory clientHttpFactory, IHinh2D hinh1, IHinh2D hinh2, ILogger<HomeController> logger)
        {
            _clientHttpFactory = clientHttpFactory;
            Logger = logger;

            Logger.LogDebug(hinh1.GetGuid() + "-" + hinh1.ToString(), null);
            Logger.LogDebug(hinh2.GetGuid() + "-" + hinh2.ToString(), null);
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> About()
        {
            HttpClient client = _clientHttpFactory.CreateClient("callgoogle");
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
