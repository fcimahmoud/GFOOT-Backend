
using Shared.DashboardModels;

namespace Services.Abstractions.Dashboard_Services.Abstraction
{
    public interface IDashboardService
    {
        Task UpdateFactoryAsync(UpdateFactoryDto updateFactoryDto, string factoryId);
        Task DeleteFactoryAsync(string factoryId);

        Task UpdateAgentAsync(UpdateAgentDto updateAgentDto, string agentId);
        Task DeleteAgentAsync(string agentId);


    }
}
