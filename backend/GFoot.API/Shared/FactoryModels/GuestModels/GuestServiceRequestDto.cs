
namespace Shared.FactoryModels.GuestModels
{
    public class GuestServiceRequestDto
    {
        public string Id { get; set; }
        public string OrganizationName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public DateTime SubmittedAt { get; set; }
    }
}
