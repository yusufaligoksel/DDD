using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Identity.Domain.Response;
using Identity.Infrastructure.Services.Abstract;
using MediatR;

namespace Identity.Application.Features.Auth.Commands
{
    public class RevokeRefreshTokenCommand : IRequest<GenericResult<string>>
    {
        public string RefreshToken { get; set; }
        public class RevokeRefreshTokenCommandHandler : IRequestHandler<RevokeRefreshTokenCommand, GenericResult<string>>
        {
            private readonly IAuthService _authService;
            public RevokeRefreshTokenCommandHandler(IAuthService authService)
            {
                _authService = authService;
            }

            public Task<GenericResult<string>> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
            {
                var result = _authService.RevokeRefreshToken(request.RefreshToken);
                return result;
            }
        }
    }
}
