using UniDwe.Infrastructure;
using UniDwe.Session;

namespace UniDwe.Repositories
{
    public interface IDbSessionRepository
    {
        Task<DbSession> GetSessionAsync(Guid sessionId);
        Task<int> UpdateSession(DbSession session);
        Task<int> CreateSession(Guid sessionId);
    }

    public class DbSessionRepository : IDbSessionRepository
    { 
        private readonly ApplicationDbContext _dbContext;

        public DbSessionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<int> CreateSession(Guid sessionId)
        {
            throw new NotImplementedException();
        }

        public Task<DbSession> GetSessionAsync(Guid sessionId)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateSession(DbSession session)
        {
            throw new NotImplementedException();
        }
    }
}
