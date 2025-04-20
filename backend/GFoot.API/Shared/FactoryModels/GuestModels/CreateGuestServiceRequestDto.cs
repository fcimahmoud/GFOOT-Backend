
namespace Shared.FactoryModels.GuestModels
{
    public class CreateGuestServiceRequestDto
    {
        public string OrganizationName { get; set; } = default!;
        [EmailAddress]
        public string Email { get; set; } = default!;
        [Phone]
        public string Phone { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string City { get; set; } = default!;
    }
}
