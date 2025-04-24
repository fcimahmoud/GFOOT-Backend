
namespace Shared.AgentModels
{
    public class FactoryWithCarbonFootprintDto
    {
        public string FactoryId { get; set; } = null!;
        public string FactoryName { get; set; } = null!;
        public float TotalCarbonEmission { get; set; }
    }
}
