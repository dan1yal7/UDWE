using Microsoft.EntityFrameworkCore;
using UniDwe.Infrastructure;
using UniDwe.Session;
using UniDwe.Models;


namespace UniDwe.Repositories
{
    public interface IUserToken
    {
        Task<Guid> CreateTokenAsync(int userId);
        Task<int?> GetTokenAsync(Guid tokenId);
    }

    public class UserTok : IUserToken
    {
        private readonly ApplicationDbContext _dbContext; 
        
        public UserTok(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Guid> CreateTokenAsync(int userId)
        {
            var token = new UserToken
            {
                UserTokenId = Guid.NewGuid(),
                Created = DateTime.UtcNow,
                UserId = userId
            };

            _dbContext.Add(token);
            await _dbContext.SaveChangesAsync();

            return token.UserTokenId;
        }

        public async Task<int?> GetTokenAsync(Guid tokenId)
        {
            var token = await _dbContext.usertokens
                .Where(s => s.UserTokenId == tokenId)
                .FirstOrDefaultAsync();

            return token?.UserId;
        }
    }
}
