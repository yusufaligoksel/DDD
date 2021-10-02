using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Domain.Entities;
using Identity.Infrastructure.Repository;
using Identity.Infrastructure.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Services.Concrete
{
    public class UserRoleService:BaseService<UserRole>,IUserRoleService
    {
        private readonly IRepository<UserRole> _repository;
        public UserRoleService(IRepository<UserRole> repository) : base(repository)
        {
            _repository = repository;
        }


        public async Task<List<UserRole>> GetRolesByUserId(int userId)
        {
            return await _repository.Table.Include(x => x.Role).Where(x => x.UserId == userId).ToListAsync();
        }
    }
}