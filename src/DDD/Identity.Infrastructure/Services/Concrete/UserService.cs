using Identity.Domain.Entities;
using Identity.Infrastructure.Repository;
using Identity.Infrastructure.Services.Abstract;

namespace Identity.Infrastructure.Services.Concrete
{
    public class UserService:BaseService<User>,IUserService
    {
        private readonly IRepository<User> _repository;
        public UserService(IRepository<User> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}