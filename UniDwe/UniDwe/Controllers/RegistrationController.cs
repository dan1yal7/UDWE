using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Internal;
using UniDwe.AutoMapper;
using UniDwe.Models.ViewModel;
using UniDwe.Services;

namespace UniDwe.Controllers
{
    [Controller]
    public class RegistrationController : Controller
    {
        private readonly IRegistrationSerivce _registrationService;
        public RegistrationController(IRegistrationSerivce registrationSerivce) 
        {
            _registrationService = registrationSerivce;
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


            if (ModelState.IsValid)
            {
                await _registrationService.CreateUserAsync(RegistrationMapper.MapRegistrationViewModelToUserModel(model));
                return Redirect("/");
            }
            return View("Index", model);
        }
    }
}
