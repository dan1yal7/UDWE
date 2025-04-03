using UniDwe.Helpers;
using UniDwe.Infrastructure;
using UniDwe.Models;
using UniDwe.Session;

namespace UniDwe.Repositories
{
    public interface IRegistrationRepository
    {
        Task<User> CreateAsync(User user);
        Task<User> GetByIdAsync(int id);
        Task<User> AuthenticateUserAsync(string username, string password, bool rememberMe);
        void Login(int id);
    }

    public class RegistrationRepository : IRegistrationRepository
    { 
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IPasswordHelper _passwordHelper;

        public RegistrationRepository(ApplicationDbContext dbContext, 
            IHttpContextAccessor contextAccessor,
            IPasswordHelper passwordHelper)
        {
            _dbContext = dbContext;
            _contextAccessor = contextAccessor;
            _passwordHelper = passwordHelper;
        }

        public async Task<User> CreateAsync(User user)
        { 
          user.Salt = Guid.NewGuid().ToString();
          user.PasswordHash = _passwordHelper.HashPassword(user.PasswordHash!, user.Salt);
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
            return await _dbContext.users.FindAsync(id) ?? throw new Exception();
        }

        public async Task<User> AuthenticateUserAsync(string username, string password, bool rememberMe)
        {
            var user = await _dbContext.users.FindAsync(username);
            if (user!.PasswordHash == _passwordHelper.HashPassword(password, user.Salt!))
            {
                Login(user.Id);
                return user;
            }
            else
            {
                throw new Exception("Incorrect Password or Username check and try again");
            }
        }
    }
}
