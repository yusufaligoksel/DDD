using System.Threading.Tasks;
using Identity.Domain.Dto;
using Identity.Domain.Request;
using Identity.Domain.Response;

namespace Identity.Infrastructure.Services.Abstract
{
    public interface IAuthService
    {
        Task<GenericResult<UserTokenDto>> CreateUserTokenAsync(LoginRequest request);
        Task<GenericResult<UserTokenDto>> CreateTokenByRefreshToken(string refreshToken);
        Task<GenericResult<string>> RevokeRefreshToken(string refreshToken);
        GenericResult<ClientTokenDto> CreateClientToken(ClientTokenRequest request);
    }
}