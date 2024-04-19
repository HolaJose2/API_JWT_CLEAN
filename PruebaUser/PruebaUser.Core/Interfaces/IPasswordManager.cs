namespace PruebaUser.Core.Interfaces
{
    public interface IPasswordManager
    {
        public string HashPassword(string password);

        public bool VerifyPassword(string enteredPassword, string storedPasswordHash);
    }
}
