using Microsoft.EntityFrameworkCore;
using UniDwe.Infrastructure;
using UniDwe.Models;
using UniDwe.Services;

namespace UniDwe.Repositories
{
    public interface IProfileRepository
    {
       Task<Profile> CreateProfileAsync(Profile profile);
       Task UpdateProfileAsync(int profileId);
       Task<IEnumerable<Profile>> GetProfileAsync(int userId);
    }

    public class ProfileRepository : IProfileRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IRegistrationSerivce _registrationSerivce;

        public ProfileRepository(ApplicationDbContext dbContext, IRegistrationSerivce registrationSerivce)
        {
            _dbContext = dbContext;
            _registrationSerivce = registrationSerivce;
        }

        public async Task<Profile> CreateProfileAsync(Profile profile)
        {
            try
            {
                var createdProfile = await _dbContext.profiles.AddAsync(profile);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Creation process FAILED: {ex.Message}");
            }
            return profile;
        }

        public async Task<IEnumerable<Profile>> GetProfileAsync(int userId)
        {
            try
            {
               var profile = await _dbContext.profiles.Where(p => p.UserId == userId).ToListAsync();
               return profile!;
            }
            catch(Exception ex)
            {
                throw new Exception($"Getting profile process FAILED: {ex.Message}");
            }
        }

        public async Task UpdateProfileAsync(int profileId)
        {
            try
            {
               var updatedProfile = _dbContext.profiles.Where(p => p.ProfileId == profileId).FirstOrDefault() ?? new Profile();
                if (updatedProfile != null)
                {
                    _dbContext.Entry(updatedProfile).State = EntityState.Modified;
                }
                else
                {
                    Profile profile = new Profile()
                    {
                      ProfileName = updatedProfile!.ProfileName,
                      FirstName = updatedProfile!.FirstName,
                      LastName = updatedProfile!.LastName,
                      ProfileImage = updatedProfile!.ProfileImage,
                    };
                }
                await _dbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new ArgumentNullException($"Update process FAILED: {ex.Message}");
            }
        }
    }
}
