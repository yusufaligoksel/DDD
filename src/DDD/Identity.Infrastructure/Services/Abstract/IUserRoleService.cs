using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Domain.Entities;

namespace Identity.Infrastructure.Services.Abstract
{
    public interface IUserRoleService:IBaseService<UserRole>
    {
        Task<List<UserRole>> GetRolesByUserId(int userId);
    }
}