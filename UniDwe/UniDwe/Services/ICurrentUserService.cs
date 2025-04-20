using UniDwe.Session;

namespace UniDwe.Services
{
    public interface ICurrentUserService
    {
        Task <bool> IsLoggedIn();
    }

    public class CurrentUserService : ICurrentUserService
    {
        private readonly IDbSessionService _dbSessionService;

        public CurrentUserService(IDbSessionService dbSessionService)
        {
           _dbSessionService = dbSessionService;
        } 

        public async Task <bool> IsLoggedIn()
        {
            return await _dbSessionService.IsLoggedInAsync();
        }
    }
}
