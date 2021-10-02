using System.Threading.Tasks;
using Identity.Application.Features.Auth.Commands;
using Identity.Domain.Dto;
using Identity.Domain.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        public async Task<GenericResult<UserTokenDto>> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }
    }
}