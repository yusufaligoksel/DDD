using System.Threading.Tasks;
using Identity.Application.Features.User.Commands.RegisterCommand;
using Identity.Domain.Dto;
using Identity.Domain.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    public class UserController:BaseController
    {
        private IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name="Register")]
        public async Task<IActionResult> Post([FromBody] RegisterCommand command)
        {
            var result = await _mediator.Send(command);
            return CreateActionResult(result, result.StatusCode);
        }
    }
}