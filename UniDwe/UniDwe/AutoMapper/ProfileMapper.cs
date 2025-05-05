using UniDwe.Models.ViewModel;
using UniDwe.Models;

namespace UniDwe.AutoMapper
{
    public class ProfileMapper
    {
        /// <summary>
        /// it is used to save data in the database that came from the user.
        /// </summary>
        /// <param name="registrationViewModel"></param>
        /// <returns></returns>
        public static Profile MapProfileViewModelToProfileModel(ProfileViewModel profileViewModel)
        {
            return new Profile()
            {
               ProfileName = profileViewModel.ProfileName,
               FirstName = profileViewModel.FirstName,
               LastName = profileViewModel.LastName,
               ProfileImage = profileViewModel.ProfileImage,
            };
        }

        /// <summary>
        /// it is used to transfer data for display on the frontend.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static ProfileViewModel MapProfileModelToProfileViewModel(Profile profile)
        {
            return new ProfileViewModel()
            {
               ProfileName = profile.ProfileName,
               FirstName = profile.FirstName,
               LastName = profile.LastName,
               ProfileImage = profile.ProfileImage,
            };
        }
    }
}
