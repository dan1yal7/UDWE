using UniDwe.Repositories;
using UniDwe.Session;

namespace UniDwe.Services
{
    public interface IDbSessionService
    {
        Task<DbSession> GetSessionAsync(Guid sessionId);
        Task<int> UpdateSessionAsync(DbSession session);
        Task<int> CreateSessionAsync(Guid sessionId);
    }

    public class DbSessionService : IDbSessionService
    {
        private readonly IDbSessionRepository _dbSessionRepository;

        public DbSessionService(IDbSessionRepository dbSessionRepository)
        {
            _dbSessionRepository = dbSessionRepository;
        }

        public async Task<int> CreateSessionAsync(Guid sessionId)
        {
            return await _dbSessionRepository.CreateSessionAsync(sessionId);
        }

        public async Task<DbSession> GetSessionAsync(Guid sessionId)
        {
            return await _dbSessionRepository.GetSessionAsync(sessionId);
        }

        public async Task<int> UpdateSessionAsync(DbSession session)
        {
            return await _dbSessionRepository.UpdateSessionAsync(session);
        }
    }
}
