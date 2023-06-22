using BobBreakfast.Breakfast;
using BobBreakfastAPI.Models;
using BobBreakfastAPI.ServiceErrors;
using ErrorOr;

namespace BobBreakfastAPI.Services.Breakfasts
{
    public class BreakfastService : IBreakfastService
    {
        private static readonly Dictionary<Guid, Breakfast> _breakfasts = new(); // In-memory database
        public Task<bool> CreateBreakfast(Breakfast breakfast) // Create a breakfast
        {
            _breakfasts.Add(breakfast.Id, breakfast); // Add breakfast to database
            return Task.FromResult(true); // Return true
        }

        public ErrorOr<Breakfast> GetBreakfast(Guid id) // Get a breakfast
        {
            if (!_breakfasts.ContainsKey(id)) // If breakfast does not exist
            {
                return Errors.Breakfast.NotFound; // Return null
            }
            return _breakfasts[id]; // Return breakfast
        }

        public Task<Breakfast> UpdateBreakfast(Guid id, UpsertBreakfastRequest request) // Update a breakfast
        {
            if (!_breakfasts.ContainsKey(id)) // If breakfast does not exist
            {
                return null; // Return null
            }
            _breakfasts[id] = new Breakfast(
                id: id,
                name: request.Name,
                description: request.Description,
                startDateTime: request.StartDateTime,
                endDateTime: request.EndDateTime,
                lastModifiedDateTime: DateTime.Now,
                savory: request.Savory,
                sweet: request.Sweet);
            return Task.FromResult(_breakfasts[id]); // Return breakfast

        }
        public Task<bool> DeleteBreakfast(Guid id) // Delete a breakfast
        {
            if (!_breakfasts.ContainsKey(id)) // If breakfast does not exist
            {
                return Task.FromResult(false); // Return false
            }
            _breakfasts.Remove(id); // Remove breakfast from database
            return Task.FromResult(true); // Return true if breakfast was removed
        }
    }
}
