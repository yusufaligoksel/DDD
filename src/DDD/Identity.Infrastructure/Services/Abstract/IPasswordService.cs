namespace Identity.Infrastructure.Services.Abstract
{
    public interface IPasswordService
    {
        string HashPassword(string password);

        bool VerifyHashedPassword(string hashedPassword, string providedPassword);
    }
}