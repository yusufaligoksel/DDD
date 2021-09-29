using System.Threading.Tasks;
using Identity.Domain.Entities;

namespace Identity.Infrastructure.Services.Abstract
{
    public interface IUserService:IBaseService<User>
    {
        Task<User> FindUserByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(User user, string password);
    }
}