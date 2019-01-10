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
using IHttpClientFactoryDemo.Services;

namespace IHttpClientFactoryDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _clientHttpFactory;

        public ILogger<HomeController> Logger { get; }
        
        public ILifetimeTransient lifetimeTransient {get;}
        public ILifetimeScope lifetimeScope {get;}
        public PrintService printService {get;}
        public HomeController(IHttpClientFactory clientHttpFactory, PrintService printService, ILifetimeTransient lifetimeTransient, ILifetimeScope lifetimeScope, ILogger<HomeController> logger)
        {
            _clientHttpFactory = clientHttpFactory;
            Logger = logger;
            this.lifetimeTransient = lifetimeTransient;
            this.lifetimeScope = lifetimeScope;
            this.printService = printService;
            Logger.LogDebug(lifetimeTransient.GetGuid() + "- lifetimeTransient", null);
            Logger.LogDebug(lifetimeScope.GetGuid() + "- lifetimeScope", null);
        }
        public IActionResult Index()
        {
            ViewBag.lifetimeTransient = lifetimeTransient;
            ViewBag.lifetimeScope = lifetimeScope;
            ViewBag.printService = printService;

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
