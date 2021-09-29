using System.Threading.Tasks;
using Identity.Domain.Entities;
using Identity.Infrastructure.Repository;
using Identity.Infrastructure.Services.Abstract;

namespace Identity.Infrastructure.Services.Concrete
{
    public class UserRefreshTokenService:BaseService<UserRefreshToken>,IUserRefreshTokenService
    {
        private readonly IRepository<UserRefreshToken> _repository;
        public UserRefreshTokenService(IRepository<UserRefreshToken> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<UserRefreshToken> FindByTokenAsync(string token)
        {
            return await _repository.FindAsync(x => x.RefreshToken == token);
        }
    }
}