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
    }
    public class RegistrationService : IRegistrationSerivce
    {
        private readonly IRegistrationRepository _registrationRepository;
        private readonly ILogger _logger;

        public RegistrationService(IRegistrationRepository registrationRepository, ILogger logger)
        {
            _registrationRepository = registrationRepository;
            _logger = logger;
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


    }
}
