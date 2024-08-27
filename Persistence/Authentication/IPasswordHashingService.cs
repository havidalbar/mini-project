using System;
namespace Persistence.Authentication
{
    public interface IPasswordHashingService
    {
        string HashPassword(string password);
        bool VerifyHashedPassword(string hashedPassword, string providedPassword);
    }
}

