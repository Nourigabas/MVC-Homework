using Homework_TV_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;

namespace Homework_TV_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStringLocalizer<HomeController> StringLocalizer;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger , IStringLocalizer<HomeController> StringLocalizer)
        {
            this.StringLocalizer = StringLocalizer;
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["Message"] = StringLocalizer["greeting_message"];
            return View();
        }

      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
