using UniDwe.Helpers;
using UniDwe.Infrastructure;
using UniDwe.Models;
using UniDwe.Services;
using UniDwe.Session;

namespace UniDwe.Repositories
{
    public interface IRegistrationRepository
    {
        Task<User> CreateAsync(User user);
        Task<User> GetByIdAsync(int id);
        Task<User> GetByEmailAsync(string email);
        Task Login(int id);
    }

    public class RegistrationRepository : IRegistrationRepository
    { 
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IPasswordHelper _passwordHelper;
        private readonly IDbSessionService _dbSessionService;

        public RegistrationRepository(ApplicationDbContext dbContext, 
            IHttpContextAccessor contextAccessor,
            IPasswordHelper passwordHelper,
            IDbSessionService dbSessionService)
        {
            _dbContext = dbContext;
            _contextAccessor = contextAccessor;
            _passwordHelper = passwordHelper;
            _dbSessionService = dbSessionService;
        }

        public async Task<User> CreateAsync(User user)
        { 
          user.Salt = Guid.NewGuid().ToString();
          user.PasswordHash = _passwordHelper.HashPassword(user.PasswordHash!, user.Salt);
          var createdUser =  await _dbContext.users.AddAsync(user);
          if(createdUser != null)
          {
            await Login(user.Id);
          }
           await _dbContext.SaveChangesAsync();
           return user;
        }

        public async Task Login(int id)
        {
           await _dbSessionService.SetUserIdAsync(id);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _dbContext.users.FindAsync(id) ?? throw new Exception();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return _dbContext.users.SingleOrDefault(u => u.Email == email);
        }
    }
}
