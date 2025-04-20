using Microsoft.EntityFrameworkCore;
using UniDwe.Infrastructure;
using UniDwe.Session;

namespace UniDwe.Repositories
{
    public interface IDbSessionRepository
    {
        Task<DbSession> GetSessionAsync(Guid sessionId);
        Task<int> UpdateSessionAsync(DbSession session);
        Task<int> CreateSessionAsync(Guid sessionId);
    }

    public class DbSessionRepository : IDbSessionRepository
    { 
        private readonly ApplicationDbContext _dbContext;

        public DbSessionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateSessionAsync(Guid sessionId)
        {
            sessionId = Guid.NewGuid();
            var createdSession = await _dbContext.AddAsync(sessionId);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<DbSession> GetSessionAsync(Guid sessionId)
        { 
            return await _dbContext.sessions.FindAsync(sessionId) ?? throw new Exception();
        }

        public async Task<int> UpdateSessionAsync(DbSession session)
        {
            return await _dbContext.sessions.ExecuteUpdateAsync(session => session);
        }
    }
}
