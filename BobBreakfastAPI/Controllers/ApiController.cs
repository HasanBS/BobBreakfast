using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BobBreakfastAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        protected IActionResult Problem(List<Error> errors) 
        { 
            var firstError = errors.First();

            var statusCode = firstError.Type switch
            {
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

            return Problem(
                detail: firstError.Description,
                statusCode: statusCode,
                title: firstError.Code);
        }
    }
}
