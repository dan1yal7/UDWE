using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using UniDwe.MiddleWare;
using UniDwe.Models.ViewModel;
using UniDwe.Services;

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
