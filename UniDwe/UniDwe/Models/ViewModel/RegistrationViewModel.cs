using System.ComponentModel.DataAnnotations;

namespace UniDwe.Models.ViewModel
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "The length of the username must be from 3 to 50 characters")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "The length of the password must be from 3 to 50 characters")]
        public string? Password { get; set; }
    }
}
