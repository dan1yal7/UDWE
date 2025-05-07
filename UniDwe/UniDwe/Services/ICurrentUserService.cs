using UniDwe.Helpers;
using UniDwe.Repositories;
using UniDwe.Session;

namespace UniDwe.Services
{
    public interface ICurrentUserService
    {
        Task <bool> IsLoggedIn();
        Task<int?> GetCurrentUserIdAsync();
        Task<int?> GetUserIdByToken();
    }

    public class CurrentUserService : ICurrentUserService
    {
        private readonly IDbSessionService _dbSessionService;
        private readonly IWebCookieHelper _webCookieHelper;
        private readonly IUserToken _userToken;

        public CurrentUserService(IDbSessionService dbSessionService,
            IWebCookieHelper webCookieHelper,
            IUserToken userToken)
        {
           _dbSessionService = dbSessionService;
           _webCookieHelper = webCookieHelper;
            _userToken = userToken; 
        } 

        public async Task <bool> IsLoggedIn()
        {
            bool isLoggedIn = await _dbSessionService.IsLoggedInAsync();
            if (!isLoggedIn)
            {
               int? userId = await GetUserIdByToken();
                if (userId != null)
                {
                   await _dbSessionService.SetUserIdAsync((int)userId);
                    isLoggedIn = true;
                }
            }

            return isLoggedIn;
        }

        public async Task<int?> GetUserIdByToken()
        {
           string? tokenCookie = _webCookieHelper.Get(AuthConstants.RememberMeCookieName);
            if (tokenCookie == null)
                return null;
            Guid? tokenGuid = Converter.StringToGuidDef(tokenCookie);
            if (tokenGuid == null)
                return null;

            int? userId = await _userToken.GetTokenAsync((Guid)tokenGuid);
            return userId;
           //Guid token
        }

        public async Task<int?> GetCurrentUserIdAsync()
        {
           return await _dbSessionService.GetUserIdAsync();
        }
    }
}
