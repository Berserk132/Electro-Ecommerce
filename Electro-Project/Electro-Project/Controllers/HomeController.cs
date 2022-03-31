using Electro_Project.Controllers.BaseController;
using Electro_Project.Models;
using Electro_Project.Models.Cart;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Electro_Project.Controllers
{
    public class HomeController : MainController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ShoppingCart shoppingCart) : base(shoppingCart)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
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