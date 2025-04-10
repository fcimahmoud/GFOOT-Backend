
using Shared.DashboardModels;

namespace Services.Abstractions.Dashboard_Services.Abstraction
{
    public interface IDashboardService
    {
        Task<UserResultDTO> AddFactoryAsync(RegisterDTO registerDTO);
        Task UpdateFactoryAsync(UpdateFactoryDto updateFactoryDto, string factoryId);
        Task DeleteFactoryAsync(string factoryId);

        Task<UserResultDTO> AddAgentAsync(RegisterDTO registerDTO);
        Task UpdateAgentAsync(UpdateAgentDto updateAgentDto, string agentId);
        Task DeleteAgentAsync(string agentId);


    }
}
