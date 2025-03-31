using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace UniDwe.Helpers
{
    public interface IPasswordHelper
    {
        string HashPassword(string password, string salt);
    }

    public class PasswordHelper : IPasswordHelper
    {
        public string HashPassword(string password, string salt)
        {  
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(password, 
            System.Text.Encoding.ASCII.GetBytes(salt),
            KeyDerivationPrf.HMACSHA512, 5000, 64));
        }
    }
}
