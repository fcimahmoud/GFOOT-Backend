
namespace Domain.Entities.Factory
{
    public class FactoriesEnvironmentalAgents
    {
        public string? FactoryUserId { get; set; }
        public virtual FactoryUser? Factory { get; set; }

        public string? EnvironmentalAgentId { get; set; }
        public virtual EnvironmentalAgent? EnvironmentalAgent { get; set; }
    }
}
