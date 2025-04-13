using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using UniDwe.AutoMapper;
using UniDwe.Helpers;
using UniDwe.Models.ViewModel;
using UniDwe.Services;

namespace UniDwe.Controllers
{
    public class LoginController : Controller
    {
        private readonly IRegistrationSerivce _registrationService;
        private readonly IPasswordHelper _passwordHelper;

        public LoginController(IRegistrationSerivce registrationService, IPasswordHelper passwordHelper)
        {
            _registrationService = registrationService;
            _passwordHelper = passwordHelper;
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
            try
            {
                var email = new MailAddress(model.Email!);
                if (email.Address != model.Email) { ModelState.AddModelError("Email", "Invalid email format"); }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Email", "Invalid email format");
                throw new Exception($"Processing failed: {ex.Message}");
            }
            try
            {
                var user = await _registrationService.GetUserByEmailAsync(model.Email!);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "User not registered/Invalid email address");
                }
                else
                {
                    var inputHashed = _passwordHelper.HashPassword(model.Password!, salt:user.Salt!);
                    if (user.PasswordHash != inputHashed) { ModelState.AddModelError(string.Empty, "Incorrect Password"); }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "User not registered/Invalid email address");
                throw new Exception($"Processing failed: {ex.Message}");
            }
            if (ModelState.IsValid)
            {
               await _registrationService.AuthenticateUserAsync(model.Email!, model.Password!, model.RememberMe);
               return Redirect("/");
            }
            return View("Index", model);
        }
    }
}
