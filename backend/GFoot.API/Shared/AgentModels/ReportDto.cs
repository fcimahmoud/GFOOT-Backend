
namespace Shared.AgentModels
{
    public class ReportDto
    {
        public string Id { get; set; }
        public string ReportBody { get; set; }
        public DateTime DateGenerated { get; set; }
        public bool OnLatestCalculation { get; set; }
        public string FactoryName { get; set; }
        public string FactoryEmail { get; set; }
        public string AgentName { get; set; }
        public string AgentEmail { get; set; }
    }
}
