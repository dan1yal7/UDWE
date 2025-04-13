using System.ComponentModel.DataAnnotations;

namespace UniDwe.Models.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
        public string? Salt { get; set; }
        public bool RememberMe { get; set; }
    }
}
