using Shared.FactoryModels.GuestModels;

namespace Services.FactoryServices
{
    public class GuestRequestService(
        IUnitOfWork unitOfWork
    ) : IGuestRequestService
    {
        public async Task SubmitRequestAsync(CreateGuestServiceRequestDto dto)
        {
            var repo = unitOfWork.GetRepository<GuestServiceRequest, string>();

            var request = new GuestServiceRequest
            {
                Id = Guid.NewGuid().ToString(),
                OrganizationName = dto.OrganizationName,
                Email = dto.Email,
                Phone = dto.Phone,
                Country = dto.Country,
                City = dto.City,
            };

            await repo.AddAsync(request);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<GuestServiceRequestDto>> GetAllAsync()
        {
            var repo = unitOfWork.GetRepository<GuestServiceRequest, string>();
            var all = await repo.GetAllAsync();
            return all.Select(r => new GuestServiceRequestDto
            {
                Id = r.Id,
                OrganizationName = r.OrganizationName,
                Email = r.Email,
                Phone = r.Phone,
                SubmittedAt = r.SubmittedAt,
            });
        }
    }

}
