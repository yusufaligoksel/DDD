using System.Threading.Tasks;
using Identity.Domain.Entities;

namespace Identity.Infrastructure.Services.Abstract
{
    public interface IUserService:IBaseService<User>
    {
        Task<User> FindUserByEmailAsync(string email);
        bool CheckPassword(User user, string password);
        Task<bool> CheckUser(string email);
    }
}