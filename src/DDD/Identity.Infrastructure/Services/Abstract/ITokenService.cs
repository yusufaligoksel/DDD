using Identity.Domain.Dto;
using Identity.Domain.Entities;

namespace Identity.Infrastructure.Services.Abstract
{
    public interface ITokenService
    {
        UserTokenDto CreateUserToken(User user);
        ClientTokenDto CreateClientToken(Client client);
    }
}