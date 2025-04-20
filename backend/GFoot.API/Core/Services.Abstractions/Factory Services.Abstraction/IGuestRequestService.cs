
using Shared.FactoryModels.GuestModels;

namespace Services.Abstractions.Factory_Services.Abstraction
{
    public interface IGuestRequestService
    {
        Task SubmitRequestAsync(CreateGuestServiceRequestDto dto);
        Task<IEnumerable<GuestServiceRequestDto>> GetAllAsync();
    }
}
