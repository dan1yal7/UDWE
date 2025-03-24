using System.Runtime.CompilerServices;
using UniDwe.Models;
using UniDwe.Repositories;

namespace UniDwe.Services
{
    public interface IRegistrationSerivce
    {
        Task<User> CreateUserAsync(User user); 
        Task<User> GetUserByIdAsync(int id); 
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

        public Task<User> CreateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
