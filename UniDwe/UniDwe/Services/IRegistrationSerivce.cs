using System.Runtime.CompilerServices;
using UniDwe.Models;
using UniDwe.Repositories;

namespace UniDwe.Services
{
    public interface IRegistrationSerivce
    {
        Task<User> CreateUserAsync(User user); 
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByEmailAsync(string email);
        Task<User>GetUserByUsernameAndPasswordAsync(string username, string password);
    }
    public class RegistrationService : IRegistrationSerivce
    {
        private readonly IRegistrationRepository _registrationRepository;

        public RegistrationService(IRegistrationRepository registrationRepository)
        {
            _registrationRepository = registrationRepository;
        }

        public async Task<User> CreateUserAsync(User user)
        { 
            return await _registrationRepository.CreateAsync(user);
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        { 
            return await _registrationRepository.GetUserByEmailAsync(email);
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _registrationRepository.GetByIdAsync(id);
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByUsernameAndPasswordAsync(string username, string password)
        { 
            return await _registrationRepository.GetByUsernameAndPasswordAsync(username, password);
            throw new NotImplementedException();
        }
    }
}
