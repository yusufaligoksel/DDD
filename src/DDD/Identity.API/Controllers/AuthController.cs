using System.Threading.Tasks;
using Identity.Application.Features.Auth.Commands;
using Identity.Domain.Dto;
using Identity.Domain.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    public class AuthController:BaseController
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost(Name="Login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);
            return CreateActionResult(result, result.StatusCode);
        }

        [HttpPost(Name = "CreateUserTokenByRefreshToken")]
        public async Task<IActionResult> CreateUserTokenByRefreshToken([FromBody] CreateUserTokenByRefreshTokenCommand command)
        {
            var result = await _mediator.Send(command);
            return CreateActionResult(result, result.StatusCode);
        }

        [HttpPost(Name="createClientToken")]
        public async Task<IActionResult> CreateClientToken([FromBody] CreateClientTokenCommand command)
        {
            var result = await _mediator.Send(command);
            return CreateActionResult(result, result.StatusCode);
        }

        [HttpPost(Name = "revokeRefreshToken")]
        public async Task<IActionResult> RevokeRefreshToken([FromBody] RevokeRefreshTokenCommand command)
        {
            var result = await _mediator.Send(command);
            return CreateActionResult(result, result.StatusCode);
        }

    }
}