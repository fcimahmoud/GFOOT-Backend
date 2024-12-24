
namespace Domain.Entities.Individual
{
    public class Notification : BaseEntity<string>
    {
        public required string NotificationType { get; set; }
        public required string NotificationBody { get; set; }
        public DateTime DateSent { get; set; } = DateTime.UtcNow;

        // Navigational Property
        public string? UserId { get; set; }
        public virtual IndividualUser? User { get; set; }
    }
}
