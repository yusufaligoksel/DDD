using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController:Controller
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