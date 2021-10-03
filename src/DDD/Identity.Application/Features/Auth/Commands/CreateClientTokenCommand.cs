using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Identity.Domain.Dto;
using Identity.Domain.Entities;
using Identity.Domain.Response;
using Identity.Infrastructure.Services.Abstract;
using MediatR;
using Microsoft.Extensions.Options;

namespace Identity.Application.Features.Auth.Commands
{
    public class CreateClientTokenCommand : IRequest<GenericResult<ClientTokenDto>>
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

        public class
            CreateClientTokenCommandHandler : IRequestHandler<CreateClientTokenCommand, GenericResult<ClientTokenDto>>
        {
            private readonly ITokenService _tokenService;
            private readonly List<Client> _clients;

            public CreateClientTokenCommandHandler(ITokenService tokenService,
                IOptions<List<Client>> clients)
            {
                _tokenService = tokenService;
                _clients = clients.Value;
            }

            public async Task<GenericResult<ClientTokenDto>> Handle(CreateClientTokenCommand request,
                CancellationToken cancellationToken)
            {
                var client =
                    _clients.SingleOrDefault(x => x.Id == request.ClientId && x.Secret == request.ClientSecret);
                
                if (client == null)
                    return GenericResult<ClientTokenDto>.ErrorResponse(new ErrorResult("Client bilgileri hatalı."),
                        (int)HttpStatusCode.BadRequest);

                var result = _tokenService.CreateClientToken(client);

                return GenericResult<ClientTokenDto>.SuccessResponse(result, 200);
            }
        }
    }
}