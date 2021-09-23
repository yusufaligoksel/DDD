using Identity.Domain.Entities;
using Identity.Infrastructure.Repository;
using Identity.Infrastructure.Services.Abstract;

namespace Identity.Infrastructure.Services.Concrete
{
    public class RoleService:BaseService<Role>,IRoleService
    {
        private readonly IRepository<Role> _repository;
        public RoleService(IRepository<Role> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}