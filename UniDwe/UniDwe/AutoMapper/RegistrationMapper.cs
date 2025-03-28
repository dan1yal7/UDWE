using UniDwe.Models;
using UniDwe.Models.ViewModel;

namespace UniDwe.AutoMapper
{
    public class RegistrationMapper
    {
        /// <summary>
        /// it is used to save data in the database that came from the user.
        /// </summary>
        /// <param name="registrationViewModel"></param>
        /// <returns></returns>
        public static User MapRegistrationViewModelToUserModel(RegistrationViewModel registrationViewModel)
        {
            return new User()
            {
                UserName = registrationViewModel.UserName,
                Email = registrationViewModel.Email!,
                PasswordHash = registrationViewModel.Password!,
            };
        }

        /// <summary>
        /// it is used to transfer data for display on the frontend.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static RegistrationViewModel MapUserModelToRegistrationViewModel(User user)
        {
            return new RegistrationViewModel()
            {
                UserName = user.UserName,
                Email = user.Email,
                Password = user.PasswordHash,
            };
        }
    }
}
