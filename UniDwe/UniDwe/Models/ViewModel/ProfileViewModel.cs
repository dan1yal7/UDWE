using System.ComponentModel.DataAnnotations;

namespace UniDwe.Models.ViewModel
{
    public class ProfileViewModel
    {
        [Required]
        public string? ProfileName { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }

        public string? ProfileImage { get; set; }
    }
}
