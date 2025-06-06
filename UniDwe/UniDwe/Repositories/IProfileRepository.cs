﻿using Microsoft.EntityFrameworkCore;
using UniDwe.Infrastructure;
using UniDwe.Models;
using UniDwe.Services;

namespace UniDwe.Repositories
{
    public interface IProfileRepository
    {
       Task<Profile> CreateProfileAsync(Profile profile);
       Task UpdateProfileAsync(Profile profile);
       Task<IEnumerable<Profile>> GetProfileAsync(int? userId);
    }

    public class ProfileRepository : IProfileRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<ProfileRepository> _logger;

        public ProfileRepository(ApplicationDbContext dbContext, IRegistrationSerivce registrationSerivce, ILogger<ProfileRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
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

        public async Task<IEnumerable<Profile>> GetProfileAsync(int? userId)
        {
            try
            {
                var profile = await _dbContext.profiles.Where(p => p.UserId == userId).ToListAsync();
                if (profile.Count == 0)
                {
                    _logger.LogError($"{profile}");
                }
                return profile!;
            }
            catch(Exception ex)
            { 
                _logger.LogError($"Smt wrong bro: {ex.Message}");
                throw new Exception($"Getting profile process FAILED: {ex.Message}");
            }
        }

        public async Task UpdateProfileAsync(Profile profile)
        {
            try
            {
               var updatedProfile = _dbContext.profiles.Where(p => p.ProfileId == profile.ProfileId).FirstOrDefault() ?? new Profile();
                
               if (updatedProfile != null)
               {
                    updatedProfile.ProfileName = profile.ProfileName;
                    updatedProfile.FirstName = profile.FirstName;
                    updatedProfile.LastName = profile.LastName;  
                    updatedProfile.ProfileImage = profile.ProfileImage;

                    _dbContext.Entry(updatedProfile).State = EntityState.Modified;
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
