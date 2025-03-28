using Microsoft.AspNetCore.Mvc;
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
        [Route("/register")]
        public IActionResult Index()    
        {
            return View("Index", new RegistrationViewModel());
        }

        [HttpPost]
        [Route("/register")]
        public IActionResult IndexSave(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
               _registrationService.CreateUserAsync(RegistrationMapper.MapRegistrationViewModelToUserModel(model)); 
            }
            return View("Index", model);
        }
    }
}
