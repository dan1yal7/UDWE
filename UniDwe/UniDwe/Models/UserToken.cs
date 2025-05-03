using System.ComponentModel.DataAnnotations;

namespace UniDwe.Models
{
    public class UserToken
    {
        [Key]
        public Guid UserTokenId { get; set; }
        public int UserId { get; set; }
        public DateTime Created {  get; set; }
    }
}
