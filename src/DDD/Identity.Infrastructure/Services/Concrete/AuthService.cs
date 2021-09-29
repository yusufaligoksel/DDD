using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Identity.Domain.Dto;
using Identity.Domain.Entities;
using Identity.Domain.Request;
using Identity.Domain.Response;
using Identity.Infrastructure.Services.Abstract;
using Microsoft.Extensions.Options;

namespace Identity.Infrastructure.Services.Concrete
{
    public class AuthService:IAuthService
    {
        private readonly List<Client> _clients;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        public AuthService(IOptions<List<Client>> clientOptions,
            ITokenService tokenService,
            IUserService userService)
        {
            _clients = clientOptions.Value;
            _tokenService = tokenService;
            _userService = userService;
        }
        
        public async Task<GenericResult<UserTokenDto>> CreateUserTokenAsync(LoginRequest request)
        {
            var user = await _userService.FindUserByEmailAsync(request.Email);
            if (user==null)
            {
                //todo:Hata mesajı dönülecek
            }

            var result = new GenericResult<UserTokenDto>();

            return result;
        }
    }
}