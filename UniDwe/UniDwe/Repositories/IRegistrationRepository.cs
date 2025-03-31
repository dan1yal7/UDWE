using UniDwe.Infrastructure;
using UniDwe.Models;
using UniDwe.Session;

namespace UniDwe.Repositories
{
    public interface IRegistrationRepository
    {
        Task<User> CreateAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetByIdAsync(int id);
        void Login(int id);
    }

    public class RegistrationRepository : IRegistrationRepository
    { 
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _contextAccessor;

        public RegistrationRepository(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor)
        {
            _dbContext = dbContext;
            _contextAccessor = contextAccessor;
        }

        public async Task<User> CreateAsync(User user)
        { 
          var createdUser =  await _dbContext.users.AddAsync(user);
          if(createdUser != null)
          {
             Login(user.Id);
          }
           await _dbContext.SaveChangesAsync();
           return user;
        }

        public void Login(int id)
        {
            _contextAccessor.HttpContext?.Session.Set(AuthConstants.AUTH_SESSION_PARAM_NAME, id);
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
