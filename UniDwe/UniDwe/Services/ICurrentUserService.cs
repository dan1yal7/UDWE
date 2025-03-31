using UniDwe.Session;

namespace UniDwe.Services
{
    public interface ICurrentUserService
    {
        bool IsLoggedIn();
    }

    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CurrentUserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        } 

        public bool IsLoggedIn()
        {
            return _contextAccessor.HttpContext?.Session.Get(AuthConstants.AUTH_SESSION_PARAM_NAME) != null;
        }
    }
}
