using Microsoft.AspNetCore.Mvc;
using UniDwe.Services;

namespace UniDwe.ViewComponents
{
    public class MainMenuViewComponent : ViewComponent
    { 
        private readonly ICurrentUserService _currentUserService;
        public MainMenuViewComponent(ICurrentUserService currentUserService)
        {
           _currentUserService = currentUserService;
        }

        public async Task <IViewComponentResult> InvokeAsync()
        {
            bool isLoggedIn = await _currentUserService.IsLoggedIn();
            return View(isLoggedIn);
        }

    }
}
