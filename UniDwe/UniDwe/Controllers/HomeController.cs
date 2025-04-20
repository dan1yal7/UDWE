using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UniDwe.Models;
using UniDwe.Services;

namespace UniDwe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICurrentUserService _currentUserService;

        public HomeController(ILogger<HomeController> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task<IActionResult> Index()
        {
           var isLoggedIn = await _currentUserService.IsLoggedIn();
           return View(isLoggedIn);
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
