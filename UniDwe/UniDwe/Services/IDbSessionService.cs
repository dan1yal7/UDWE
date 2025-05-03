using UniDwe.Helpers;
using UniDwe.Repositories;
using UniDwe.Session;

namespace UniDwe.Services
{
    public interface IDbSessionService
    {
        Task<DbSession> GetSessionAsync();
        Task<int?> SetUserIdAsync(int userId);
        Task<int?> GetUserIdAsync();
        Task<bool> IsLoggedInAsync();
    }

    public class DbSessionService : IDbSessionService
    {
        private readonly IDbSessionRepository _dbSessionRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DbSessionService(IDbSessionRepository dbSessionRepository, IHttpContextAccessor httpContextAccessor)
        {
            _dbSessionRepository = dbSessionRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public void CreateSessionCookie(Guid sessionId)
        {
            CookieOptions options = new CookieOptions();
            options.Path = "/";
            options.HttpOnly = true;
            options.Secure = true;
            options.Expires = DateTimeOffset.UtcNow.AddSeconds(5);
           _httpContextAccessor?.HttpContext?.Response.Cookies.Delete(AuthConstants.SessionCookieName);
           _httpContextAccessor?.HttpContext?.Response.Cookies.Append(AuthConstants.SessionCookieName, sessionId.ToString(), options);
        }
        
        private async Task<DbSession> CreateSessionAsync()
        {
            var data = new DbSession()
            {
                SessionId = Guid.NewGuid(),
                Created = DateTime.UtcNow,
                LastAccessed = DateTime.UtcNow,
            };
            await _dbSessionRepository.CreateSessionAsync(data);
            return data;
        }

        private DbSession? dbSession = null;
        public async Task<DbSession> GetSessionAsync()
        { 
            if (dbSession != null)
            {
                return dbSession;
            }

            Guid sessionId;
            var cookie = _httpContextAccessor.HttpContext?.Request.Cookies.FirstOrDefault(m => m.Key == AuthConstants.SessionCookieName);

            if (cookie != null && Guid.TryParse(cookie.Value.Value, out sessionId)) { } 
            else  sessionId = Guid.NewGuid(); { } 

            var data = await _dbSessionRepository.GetSessionAsync(sessionId);
            if (data == null)
            {
                data = await this.CreateSessionAsync();
                CreateSessionCookie(data.SessionId);
            }
            dbSession = data;
            return data;
        }

        public async Task<int?> SetUserIdAsync(int userId)
        {
            var data =  await this.GetSessionAsync();
            data.UserId = userId;
            data.SessionId = Guid.NewGuid();
            CreateSessionCookie(data.SessionId);
            return await _dbSessionRepository.CreateSessionAsync(data);
        }

        public async Task<int?> GetUserIdAsync()
        {
            var data = await this.GetSessionAsync();
            return data.UserId;
        }

        public async Task<bool> IsLoggedInAsync()
        {
            var data = await this.GetSessionAsync();
            return data.UserId != null;
        }
    }
}
