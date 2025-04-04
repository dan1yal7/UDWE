using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;
using UniDwe.Helpers;
using UniDwe.Models;
using UniDwe.Repositories;

namespace UniDwe.Services
{
    public interface IRegistrationSerivce
    {
        Task<User> CreateUserAsync(User user); 
        Task<User> GetUserByIdAsync(int id);
        Task<User> AuthenticateUserAsync(string email, string password, bool rememberMe);
    }
    public class RegistrationService : IRegistrationSerivce
    {
        private readonly IRegistrationRepository _registrationRepository;
        private readonly IPasswordHelper _passwordHelper;

        public RegistrationService(IRegistrationRepository registrationRepository, IPasswordHelper passwordHelper)
        {
            _registrationRepository = registrationRepository;
            _passwordHelper = passwordHelper;
            
        }

        public async Task<User> AuthenticateUserAsync(string email, string password, bool rememberMe)
        {

            var user = await _registrationRepository.GetByEmailAsync(email);
            if (user.PasswordHash != _passwordHelper.HashPassword(user.PasswordHash!, user.Salt!))
            {
                _registrationRepository.Login(user.Id);
            }
            return user;
        }

        public async Task<User> CreateUserAsync(User user)
        { 
            return await _registrationRepository.CreateAsync(user);
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _registrationRepository.GetByIdAsync(id);
            throw new NotImplementedException();
        }
    }
}
