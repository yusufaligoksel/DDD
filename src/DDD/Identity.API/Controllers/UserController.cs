using System.Threading.Tasks;
using Identity.Application.Features.User.Commands.RegisterCommand;
using Identity.Domain.Dto;
using Identity.Domain.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController
    {
        private IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<GenericResult<UserDto>> Post([FromBody] RegisterCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }
    }
}