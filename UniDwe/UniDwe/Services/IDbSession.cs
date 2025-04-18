using UniDwe.Session;

namespace UniDwe.Services
{
    public interface IDbSession
    {
        Task<DbSession> GetSessionAsync(Guid sessionId);
        Task<int> UpdateSession(DbSession session);
        Task<int> CreateSession(Guid sessionId);
    }

    public class DbSess : IDbSession
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
