using Microsoft.AspNetCore.Mvc;

namespace Management.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BaseController: ControllerBase
    {
        protected IActionResult CreateActionResult<T>(T response, int statusCode) where T : class
        {
            return new ObjectResult(response)
            {
                StatusCode = statusCode
            };
        }
    }
}