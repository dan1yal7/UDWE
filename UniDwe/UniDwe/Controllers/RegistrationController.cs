using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Internal;
using System.Net.Mail;
using UniDwe.AutoMapper;
using UniDwe.Infrastructure;
using UniDwe.Models.ViewModel;
using UniDwe.Services;
using UniDwe.MiddleWare;

namespace UniDwe.Controllers
{
    [NonAuthorize]
    [Controller]
    public class RegistrationController : Controller
    {
        private readonly IRegistrationSerivce _registrationService;
        private readonly ApplicationDbContext _dbContext;
        public RegistrationController(IRegistrationSerivce registrationSerivce, ApplicationDbContext dbContext) 
        {
            _registrationService = registrationSerivce;
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("/registration")]
        public IActionResult Index()    
        {
            return View("Index", new RegistrationViewModel());
        }

        [HttpPost]
        [Route("/registration")]
        public async Task <IActionResult> IndexSave(RegistrationViewModel model)
        {
            if (model.UserName == model.Email) { ModelState.AddModelError("Username", "The Username and Email address must not match."); }
            if (model.UserName!.Length > 50 || model.UserName!.Length <=5) { ModelState.AddModelError("UserName", "The length of the username must be from 5 to 50 characters"); }
            if (model.Password!.Length > 50 || model.Password!.Length <=8) { ModelState.AddModelError("Password", "The length of the password must be from 8 to 50 characters"); }

            var email = new MailAddress(model.Email!);
            if (email.Address != model.Email) { ModelState.AddModelError("Email", "Invalid email format"); }

            var isEmailAlreadyExists = _dbContext.users.Any(x => x.Email == model.Email);
            if (isEmailAlreadyExists)
            {
                ModelState.AddModelError("Email", "User with this email already exists");
            }

            if (ModelState.IsValid)
            {
                await _registrationService.CreateUserAsync(RegistrationMapper.MapRegistrationViewModelToUserModel(model));
                return Redirect("/");
            }
            return View("Index", model);
        }
    }
}
