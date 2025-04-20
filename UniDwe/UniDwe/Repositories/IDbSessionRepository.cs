using Microsoft.EntityFrameworkCore;
using UniDwe.Infrastructure;
using UniDwe.Session;

namespace UniDwe.Repositories
{
    public interface IDbSessionRepository
    {
        Task<DbSession> GetSessionAsync(Guid sessionId);
        Task<int> UpdateSessionAsync(DbSession session);
        Task<int> CreateSessionAsync(DbSession session);
    }

    public class DbSessionRepository : IDbSessionRepository
    { 
        private readonly ApplicationDbContext _dbContext;

        public DbSessionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateSessionAsync(DbSession session)
        {
            var createdSession = await _dbContext.AddAsync(session);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<DbSession?> GetSessionAsync(Guid sessionId)
        { 
            return _dbContext.sessions.FirstOrDefault(s => s.SessionId == sessionId);
        }

        public async Task<int> UpdateSessionAsync(DbSession session)
        {
            return await _dbContext.sessions.ExecuteUpdateAsync(session => session);
        }
    }
}
