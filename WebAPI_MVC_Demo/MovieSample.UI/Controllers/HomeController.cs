using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieSample.UI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieSample.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //using HttpClient client = new HttpClient(); //Führt zu der SocketException 
            
            


            return View();
        } //client.Dispose() Objekt baut nicht sofort ab .-> weil die Socket verzögert freigegeben werden

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
