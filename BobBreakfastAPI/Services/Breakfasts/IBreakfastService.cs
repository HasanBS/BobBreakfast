using BobBreakfast.Breakfast;
using BobBreakfastAPI.Models;
using ErrorOr;

namespace BobBreakfastAPI.Services.Breakfasts
{
    public interface IBreakfastService
    {
        Task<bool> CreateBreakfast(Breakfast breakfast);
        ErrorOr<Breakfast> GetBreakfast(Guid id);

        Task<Breakfast> UpdateBreakfast(Guid id, UpsertBreakfastRequest request);

        Task<bool> DeleteBreakfast(Guid id);
    }
}
