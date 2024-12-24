
namespace Domain.Entities.Factory
{
    public class FactoryUser : BaseEntity<string>
    {
        public string? IndustryType { get; set; }

        // Navigational Property
        public string? ApplicationUserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }
        public virtual ICollection<FactoriesEnvironmentalAgents>? FactoryOrganizations { get; set; }
        public virtual ICollection<FactoryEmission>? FactoryEmissions { get; set; }
        public virtual ICollection<SensorData>? SensorData { get; set; }
        public virtual ICollection<Report>? Reports { get; set; }
        public virtual ICollection<FactoryRecommendation>? FactoryRecomendations { get; set; }
    }
}
