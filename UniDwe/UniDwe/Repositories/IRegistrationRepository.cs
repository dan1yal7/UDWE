using UniDwe.Infrastructure;
using UniDwe.Models;

namespace UniDwe.Repositories
{
    public interface IRegistrationRepository
    {
        Task<User> CreateAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetByIdAsync(int id);
    }

    public class RegistrationRepository : IRegistrationRepository
    { 
        private readonly ApplicationDbContext _dbContext;

        public RegistrationRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> CreateAsync(User user)
        { 
           await _dbContext.users.AddAsync(user);
           await _dbContext.SaveChangesAsync();
           return user;
           
        }

        public async Task<User> GetByIdAsync(int id)
        { 
           return await _dbContext.users.FindAsync(id);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _dbContext.users.FindAsync(email);
        }
    }
}
