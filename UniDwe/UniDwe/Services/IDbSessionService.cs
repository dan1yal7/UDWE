using UniDwe.Session;

namespace UniDwe.Services
{
    public interface IDbSessionService
    {
        Task<DbSession> GetSessionAsync(Guid sessionId);
        Task<int> UpdateSession(DbSession session);
        Task<int> CreateSession(Guid sessionId);
    }

    public class DbSessionService : IDbSessionService
    {
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
