using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
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
    }
}
