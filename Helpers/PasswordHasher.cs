using System.Security.Cryptography;

namespace WebApplication1.Helpers
{
    public class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            // Generate a salt and hash the password using BCrypt
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            // Verify the password against the hashed password using BCrypt
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

    }
        
}
