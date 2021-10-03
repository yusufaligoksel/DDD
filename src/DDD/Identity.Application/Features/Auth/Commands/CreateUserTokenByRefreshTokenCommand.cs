using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Identity.Domain.Dto;
using Identity.Domain.Request;
using Identity.Domain.Response;
using Identity.Infrastructure.Services.Abstract;
using MediatR;

namespace Identity.Application.Features.Auth.Commands
{
    public class CreateUserTokenByRefreshTokenCommand : IRequest<GenericResult<UserTokenDto>>
    {
        public string RefreshToken { get; set; }

        public class CreateUserTokenByRefreshTokenCommandHandler:IRequestHandler<CreateUserTokenByRefreshTokenCommand, GenericResult<UserTokenDto>>
        {
            private readonly IAuthService _authService;
            public CreateUserTokenByRefreshTokenCommandHandler(IAuthService authService)
            {
                _authService = authService;
            }
            public async Task<GenericResult<UserTokenDto>> Handle(CreateUserTokenByRefreshTokenCommand request, CancellationToken cancellationToken)
            {
                var tokenResult = await _authService.CreateTokenByRefreshToken(request.RefreshToken);

                return tokenResult;
            }
        }
    }
}
