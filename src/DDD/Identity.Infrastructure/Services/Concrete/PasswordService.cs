using System;
using Identity.Infrastructure.Services.Abstract;
using BC = BCrypt.Net.BCrypt;

namespace Identity.Infrastructure.Services.Concrete
{
    public class PasswordService:IPasswordService
    {
        public string HashPassword(string password)
        {
            return BC.HashPassword(password);
        }

        public bool VerifyHashedPassword(string password, string passwordHash)
        {
            return BC.Verify(password,passwordHash);
        }
    }
}