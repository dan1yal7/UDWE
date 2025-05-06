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
        private readonly ILogger<ProfileController> _logger;
        public ProfileController(ICurrentUserService currentUserService, IProfileService profileService, ILogger<ProfileController> logger)
        {
            _currentUserService = currentUserService;
            _profileService = profileService;
            _logger = logger;
        }

        [HttpGet]
        [Route("/profile")]
        public async Task <IActionResult> Index()
        {
            int? userId = await _currentUserService.GetCurrentUserIdAsync();
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId), "User not found");
            }
            var profiles = await _profileService.GetProfileAsync((int)userId);

            Profile? profileModel = profiles.FirstOrDefault();
            if (profileModel != null)
            {
                ProfileMapper.MapProfileModelToProfileViewModel(profileModel); new ProfileViewModel();
            }    
            return View(new ProfileViewModel());
        }

        [HttpPost]
        [Route("/profile")]
        [AutoValidateAntiforgeryToken]
        public async Task <IActionResult> IndexSave(ProfileViewModel model)
        {
            try
            {
                int? userId = await _currentUserService.GetCurrentUserIdAsync();
                if (userId == null)
                {
                    throw new ArgumentNullException(nameof(userId), "User not found");
                }
                var profiles = await _profileService.GetProfileAsync((int)userId);
                if (!profiles.Any(m => m.ProfileId == model.ProfileId))
                {

                    throw new Exception();
                }

                if (ModelState.IsValid)
                {
                    Profile profileModel = ProfileMapper.MapProfileViewModelToProfileModel(model);
                    profileModel.UserId = (int)userId;
                    if (Request.Form.Files.Count > 0 && Request.Form.Files[0] != null)
                    {
                        WebFile wbf = new WebFile();
                        string filename = wbf.GetWebFileName(Request.Form.Files[0].FileName);

                        await wbf.UploadAndResizeImage(Request.Form.Files[0].OpenReadStream(), filename, 800, 600);
                        profileModel.ProfileImage = filename;
                        await _profileService.UpdateProfileAsync(profileModel.ProfileId);
                    }
                }
            }
            catch(Exception ex) 
            {
                _logger.LogError($"FAILED PROCESS: {ex.Message}");
                throw new Exception($"Process FAILED: {ex.Message}");
            }
            return View("Index", new ProfileViewModel());
        }
    }
}
