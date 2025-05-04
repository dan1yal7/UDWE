using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using UniDwe.AutoMapper;
using UniDwe.Helpers;
using UniDwe.Models.ViewModel;
using UniDwe.Services;
using UniDwe.MiddleWare;

namespace UniDwe.Controllers
{
    [NonAuthorize()]
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
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> LogIn(LoginViewModel model)
        {
            try
            {
                var enteredEmail = new MailAddress(model.Email!);
                if (enteredEmail.Address != model.Email) { ModelState.AddModelError("Email", "Invalid email format"); }
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
                    var enteredPassword = _passwordHelper.HashPassword(model.Password!, salt:user.Salt!);
                    if (user.PasswordHash != enteredPassword) { ModelState.AddModelError(string.Empty, "Incorrect Password"); }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "User not registered/Invalid email address");
                throw new Exception($"Processing failed: {ex.Message}");
            }

            if (ModelState.IsValid)
            {
               await _registrationService.AuthenticateUserAsync(model.Email!, model.Password!, model.RememberMe == true);
               return RedirectToLocalPage("/");
            }
            return View("Index", model);
        }

        public IActionResult RedirectToLocalPage(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}
