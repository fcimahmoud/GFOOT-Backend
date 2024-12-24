
namespace Domain.Entities.Factory
{
    public class Report : BaseEntity<string>
    {
        public required string ReportBody { get; set; }
        public DateTime DateGenerated { get; set; } = DateTime.UtcNow;

        // Navigational Property
        public string? FactoryUserId { get; set; }
        public virtual FactoryUser? Factory { get; set; }
        public string? EnvironmentalAgentId { get; set; }
        public virtual EnvironmentalAgent? EnvironmentalAgent { get; set; }
    }
}
