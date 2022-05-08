using Microsoft.AspNetCore.Mvc;
using QuickVisualizer.Models;
using System.Diagnostics;

namespace QuickVisualizer.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet(nameof(Privacy))]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet(nameof(Error))]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}