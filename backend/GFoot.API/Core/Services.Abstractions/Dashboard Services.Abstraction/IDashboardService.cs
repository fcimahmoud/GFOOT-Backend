
using Domain.Entities.Factory;
using Shared.DashboardModels;

namespace Services.Abstractions.Dashboard_Services.Abstraction
{
    public interface IDashboardService
    {
        Task<UserResultDTO> AddFactoryAsync(RegisterDTO registerDTO);
        Task<FactoryUserDto> GetFactoryByIdAsync(string factoryId);
        Task<IEnumerable<FactoryUserDto>> GetAllFactoryUsersAsync();
        Task UpdateFactoryAsync(UpdateFactoryDto updateFactoryDto, string factoryId);
        Task DeleteFactoryAsync(string factoryId);

        Task<AgentUserDto> GetAgentByIdAsync(string agentId);
        Task<IEnumerable<AgentUserDto>> GetAllAgentUsersAsync();
        Task<UserResultDTO> AddAgentAsync(RegisterDTO registerDTO);
        Task UpdateAgentAsync(UpdateAgentDto updateAgentDto, string agentId);
        Task DeleteAgentAsync(string agentId);


    }
}
