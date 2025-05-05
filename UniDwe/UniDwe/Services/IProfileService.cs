using UniDwe.Models;
using UniDwe.Repositories;

namespace UniDwe.Services
{
    public interface IProfileService
    {
        Task<Profile> AddProfileAsync(Profile profile);
        Task<IEnumerable<Profile>> GetProfileAsync(int userId);
        Task UpdateProfileAsync(int  profileId);
    }
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<Profile> AddProfileAsync(Profile profile)
        {
            return await _profileRepository.CreateProfileAsync(profile);
        }

        public async Task<IEnumerable<Profile>> GetProfileAsync(int userId)
        {
            return await _profileRepository.GetProfileAsync(userId);
        }

        public async Task UpdateProfileAsync(int profileId)
        {
           await _profileRepository.UpdateProfileAsync(profileId);
        }
    }
}
