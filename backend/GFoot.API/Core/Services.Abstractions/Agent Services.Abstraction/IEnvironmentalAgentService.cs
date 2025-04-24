global using Domain.Entities.Factory;
global using Shared.AgentModels;

namespace Services.Abstractions.Agent_Services.Abstraction
{
    public interface IEnvironmentalAgentService
    {
        public Task<IEnumerable<FactoryWithCarbonFootprintDto>> GetAllFactoriesWithCarbonFootprintsAsync();
        public Task<IEnumerable<FactoryWithCarbonFootprintDto>> GetFactoriesExceedingCarbonThresholdAsync(float threshold);
        public Task<string> CreateReportOfAllCalculationsAsync(string agentId, string factoryId);
        public Task<string> CreateReportOfLatestCalculationAsync(string factoryId, string agentId);
        public Task<IEnumerable<ReportDto>> GetReportsByCalculationTypeAsync(string agentId, bool onLatestCalculation);
        public Task<IEnumerable<ReportDto>> GetAllReportsOfFactoryAsync(string agentId, string factoryId, bool onLatestCalculation);
        public Task<ReportDto> GetReportByIdAsync(string reportId);
        public Task SendReportToAgentAsync(string reportId);
        //public Task SendReportToAgentAsPdfAsync(string reportId);
        public Task DeleteReportAsync(string reportId);
    }
}
