
namespace Domain.Entities.Factory
{
    public class EnvironmentalAgent : BaseEntity<string>
    {
        // Navigational Property
        public string? ApplicationUserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }

        public virtual ICollection<FactoriesEnvironmentalAgents>? FactoryOrganizations { get; set; }
        public virtual ICollection<Report>? Reports { get; set; }
    }
}
