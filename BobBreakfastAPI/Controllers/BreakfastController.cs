using BobBreakfast.Breakfast;
using BobBreakfastAPI.Models;
using BobBreakfastAPI.ServiceErrors;
using BobBreakfastAPI.Services.Breakfasts;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;



namespace BobBreakfastAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BreakfastController : ApiController
    {
        IBreakfastService _breakfastService;
        public BreakfastController(IBreakfastService breakfastService)
        {
            _breakfastService = breakfastService;
        }
        [HttpPost()]
        public IActionResult CreateBreakfast(CreateBreakfastRequest createBreakfastRequest)
        {
            var breakfast =  Breakfast.Create(
                createBreakfastRequest.Name,
                createBreakfastRequest.Description,
                createBreakfastRequest.StartDateTime,
                createBreakfastRequest.EndDateTime,
                createBreakfastRequest.Savory,
                createBreakfastRequest.Sweet);

            //TODO: Save breakfast to database

            if(breakfast.IsError)
            {
                return Problem(breakfast.Errors);
            }

            _breakfastService.CreateBreakfast(breakfast.Value);
            BreakfastResponse response = MapBreakfastResponse(breakfast.Value);

            return CreatedAtAction(
                nameof(GetBreakfasts),
                new { id = breakfast.Value.Id },
                response);
        }

        [HttpGet("{id:guid}")]
        public  IActionResult GetBreakfasts(Guid id)
        {
            ErrorOr<Breakfast> result =  _breakfastService.GetBreakfast(id);

            return result.Match(
                          breakfast => Ok(MapBreakfastResponse(breakfast)),
                          errors => Problem(errors));
        }
        [HttpPost("{id:guid}")]
        public async Task<IActionResult> UpdateBreakfast(Guid id, UpsertBreakfastRequest upsertBreakfastRequest)
        {
            var breakfast = await _breakfastService.UpdateBreakfast(id, upsertBreakfastRequest);
            return Ok(breakfast);
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteBreakfast(Guid id)
        {
            var result = await _breakfastService.DeleteBreakfast(id);
            return Ok(result);
        }

        private static BreakfastResponse MapBreakfastResponse(BobBreakfastAPI.Models.Breakfast breakfast)
        {
            return new BreakfastResponse(
                            breakfast.Id,
                            breakfast.Name,
                            breakfast.Description,
                            breakfast.StartDateTime,
                            breakfast.EndDateTime,
                            breakfast.LastModifiedDateTime,
                            breakfast.Savory,
                            breakfast.Sweet);
        }
    }
}
