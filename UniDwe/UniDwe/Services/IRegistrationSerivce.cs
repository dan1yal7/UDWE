using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;
using UniDwe.Helpers;
using UniDwe.Models;
using UniDwe.Repositories;
using UniDwe.Session;

namespace UniDwe.Services
{
    public interface IRegistrationSerivce
    {
        Task<User> CreateUserAsync(User user); 
        Task<User> GetUserByIdAsync(int id);
        Task<User> AuthenticateUserAsync(string email, string password, bool rememberMe);
        Task<User> GetUserByEmailAsync(string email);
    }
    public class RegistrationService : IRegistrationSerivce
    {
        private readonly IRegistrationRepository _registrationRepository;
        private readonly IPasswordHelper _passwordHelper;
        private readonly IDbSessionService _dbSessionService;
        private readonly IWebCookieHelper _webCookieHelper;
        private readonly IUserToken _userToken;

        public RegistrationService(IRegistrationRepository registrationRepository,
            IPasswordHelper passwordHelper,
            IUserToken userToken,
            IWebCookieHelper webCookieHelper,
            IDbSessionService dbSessionService)
        {
            _registrationRepository = registrationRepository;
            _passwordHelper = passwordHelper;
            _userToken = userToken;
            _webCookieHelper = webCookieHelper;
            _dbSessionService = dbSessionService;
        }

        public async Task<User> AuthenticateUserAsync(string email, string password, bool rememberMe)
        {
            var user = await _registrationRepository.GetByEmailAsync(email);
            if (user.PasswordHash != _passwordHelper.HashPassword(password, user.Salt!))
            {
                throw new Exception("Incorrect password");
            }
            await _registrationRepository.Login(user.Id);

            if (rememberMe)
            {
              Guid tokenId =  await _userToken.CreateTokenAsync(user.Id!);
              _webCookieHelper.AddSecure(AuthConstants.RememberMeCookieName, tokenId.ToString(), AuthConstants.RememberMeDays);
            }
            return user;
        }

        public async Task<User> CreateUserAsync(User user)
        { 
            return await _registrationRepository.CreateAsync(user);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _registrationRepository.GetByEmailAsync(email);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _registrationRepository.GetByIdAsync(id);
        }
    }
}
