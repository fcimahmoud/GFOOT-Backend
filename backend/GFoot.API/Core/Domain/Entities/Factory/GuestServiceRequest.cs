
namespace Domain.Entities.Factory
{
    public class GuestServiceRequest : BaseEntity<string>
    {
        public string OrganizationName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string City { get; set; } = default!;
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    }
}
