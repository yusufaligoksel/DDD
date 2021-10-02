using System.Threading;
using System.Threading.Tasks;
using Identity.Domain.Dto;
using Identity.Domain.Request;
using Identity.Domain.Response;
using Identity.Infrastructure.Services.Abstract;
using MediatR;

namespace Identity.Application.Features.Auth.Commands
{
    public class LoginCommand:IRequest<GenericResult<UserTokenDto>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginCommandHandler:IRequestHandler<LoginCommand,GenericResult<UserTokenDto>>
    {
        private readonly IAuthService _authService;
        public LoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<GenericResult<UserTokenDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var tokenResult = await _authService.CreateUserTokenAsync(new LoginRequest
                { Email = request.Email, Password = request.Password });

            return tokenResult;

        }
    }
}