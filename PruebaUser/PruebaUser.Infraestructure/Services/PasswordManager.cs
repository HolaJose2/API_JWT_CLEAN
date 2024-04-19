using PruebaUser.Core.Interfaces;

namespace PruebaUser.Infraestructure.Services
{
    public class PasswordManager : IPasswordManager
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password, 13);
        }

        public bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(enteredPassword, storedPasswordHash);
        }
    }
}
