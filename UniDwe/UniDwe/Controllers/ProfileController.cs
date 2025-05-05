using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using UniDwe.AutoMapper;
using UniDwe.MiddleWare;
using UniDwe.Models;
using UniDwe.Models.ViewModel;
using UniDwe.Services;

namespace UniDwe.Controllers
{
    [SiteAuthorize()]
    [Controller]
    public class ProfileController : Controller
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IProfileService _profileService;
        public ProfileController(ICurrentUserService currentUserService, IProfileService profileService)
        {
            _currentUserService = currentUserService;
            _profileService = profileService;
        }

        [HttpGet]
        [Route("/profile")]
        public async Task <IActionResult> Index(Profile model)
        {
            int? userId = await _currentUserService.GetCurrentUserIdAsync();
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId), "User not found");
            }
            var profiles = await _profileService.GetProfileAsync((int)userId);
            ProfileMapper.MapProfileModelToProfileViewModel(model);
            profiles.FirstOrDefault();
            return View(new ProfileViewModel());
        }

        [HttpPost]
        [Route("/profile")]
        [AutoValidateAntiforgeryToken]
        public async Task <IActionResult> IndexSave()
        {
            var imageData = Request.Form.Files[0];
            if (imageData != null)
            {
                WebFile wbf = new WebFile();
                string filename = wbf.GetWebFileName(imageData.FileName);

                await wbf.UploadAndResizeImage(imageData.OpenReadStream(), filename, 800, 600);
            }

            return View("Index", new ProfileViewModel());
        }
    }
}
