using System.Linq;
using System.Threading.Tasks;
using Identity.Domain.Entities;
using Identity.Infrastructure.Repository;
using Identity.Infrastructure.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Services.Concrete
{
    public class UserService:BaseService<User>,IUserService
    {
        private readonly IRepository<User> _repository;
        private readonly IPasswordService _password;
        public UserService(IRepository<User> repository,IPasswordService password) : base(repository)
        {
            _repository = repository;
            _password = password;
        }

        public async Task<User> FindUserByEmailAsync(string email)
        {
            return await _repository.FindAsync(x => x.Email == email);
        }

        public bool CheckPassword(User user, string password)
        {
            return _password.VerifyHashedPassword(password, user.PasswordHash);
        }
        
        public async Task<bool> CheckUser(string email)
        {
            return await _repository.Table.AnyAsync(x => x.Email == email);
        }
    }
}