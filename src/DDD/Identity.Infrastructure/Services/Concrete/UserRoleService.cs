using Identity.Domain.Entities;
using Identity.Infrastructure.Repository;
using Identity.Infrastructure.Services.Abstract;

namespace Identity.Infrastructure.Services.Concrete
{
    public class UserRoleService:BaseService<UserRole>,IUserRoleService
    {
        private readonly IRepository<UserRole> _repository;
        public UserRoleService(IRepository<UserRole> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}