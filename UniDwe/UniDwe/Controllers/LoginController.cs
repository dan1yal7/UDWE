using Microsoft.AspNetCore.Mvc;
using UniDwe.AutoMapper;
using UniDwe.Models.ViewModel;
using UniDwe.Services;

namespace UniDwe.Controllers
{
    public class LoginController : Controller
    {
        private readonly IRegistrationSerivce _registrationService;

        public LoginController(IRegistrationSerivce registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpGet]
        [Route("/login")]
        public IActionResult Index()
        {
            return View("Index", new LoginViewModel());
        }

        [HttpPost]
        [Route("/login")]
        public async Task <IActionResult> LogIn(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
               await _registrationService.AuthenticateUserAsync(model.Email!, model.Password!, model.RememberMe);
               return Redirect("/");
            }
            return View("Index", model);
        }
    }
}
