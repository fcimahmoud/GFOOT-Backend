global using Domain.Entities.Identity;

namespace Domain.Entities.Individual
{
    public class IndividualUser : BaseEntity<string>
    {
        // Navigational Property
        public string? ApplicationUserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }
        public virtual ICollection<Notification>? Notifications { get; set; }
        public virtual ICollection<IndividualRecommendation>? IndividualRecommendations { get; set; }
        public virtual ICollection<Activity>? Activities { get; set; }
    }
}
