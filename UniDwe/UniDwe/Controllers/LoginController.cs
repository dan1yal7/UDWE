using Microsoft.AspNetCore.Mvc;

namespace UniDwe.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
