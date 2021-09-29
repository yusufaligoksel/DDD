using System.Linq;
using System.Threading.Tasks;
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

        public async Task<User> FindUserByEmailAsync(string email)
        {
            return await _repository.FindAsync(x => x.Email == email);
        }

        public Task<bool> CheckPasswordAsync(User user, string password)
        {
            throw new System.NotImplementedException();
        }
    }
}