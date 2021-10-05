using System.Threading.Tasks;
using Management.Application.Features.Category.Commands.InsertCategoryCommad;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Management.API.Controllers
{
    [Authorize]
    public class CategoryController : BaseController
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] InsertCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            return CreateActionResult(result, result.StatusCode);
        }
    }
}