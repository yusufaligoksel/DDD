using System.Threading.Tasks;
using Identity.Domain.Entities;

namespace Identity.Infrastructure.Services.Abstract
{
    public interface IUserRefreshTokenService:IBaseService<UserRefreshToken>
    {
        Task<UserRefreshToken> FindByTokenAsync(string token);
    }
}