using Microsoft.AspNetCore.Mvc;

namespace BobBreakfastAPI.Controllers
{
    public class ErrorsController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error() 
        { 
           //TODO: Log error
           return Problem(); 
        }
    }
}
