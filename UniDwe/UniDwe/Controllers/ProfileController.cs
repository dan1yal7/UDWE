using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniDwe.MiddleWare;
using UniDwe.Models.ViewModel;

namespace UniDwe.Controllers
{
    [SiteAuthorize()]
    [Controller]
    public class ProfileController : Controller
    {
        [HttpGet]
        [Route("/profile")]
        public IActionResult Index()
        {
            return View(new ProfileViewModel());
        }

        [HttpPost]
        [Route("/profile")]
        [AutoValidateAntiforgeryToken]
        public async Task <IActionResult> Profile(ProfileViewModel model)
        {

            return View();
        }
    }
}
