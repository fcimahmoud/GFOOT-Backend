
global using Domain.Entities.Factory;
global using Shared.DashboardModels;

namespace Presentation
{
    [Authorize(Roles ="AdminRole")]
    public class DashboardController(IServiceManager serviceManager)
        : ApiController
    {
        [HttpPost("add-factory")]
        public async Task<ActionResult<UserResultDTO>> AddFactory(RegisterDTO register)
            => Ok(await serviceManager.AuthenticationService.RegisterAsync(register));

        [HttpPut("update-factory/{factoryId}")]
        public async Task<ActionResult> UpdateFactory(UpdateFactoryDto updateFactoryDto, string factoryId)
        {
            await serviceManager.DashboardService.UpdateFactoryAsync(updateFactoryDto, factoryId);
            return Ok("Factory Updated Successfully");
        }

        [HttpDelete("delete-factory/{factoryId}")]
        public async Task<ActionResult<UserResultDTO>> DeleteFactory(string factoryId)
        {
            await serviceManager.DashboardService.DeleteFactoryAsync(factoryId);
            return Ok();
        }

        [HttpGet("factories/{factoryId}")]
        public async Task<ActionResult<FactoryUserDto>> GetFactoryById(string factoryId)
        {
            var factory = await serviceManager.DashboardService.GetFactoryByIdAsync(factoryId);
            return factory is not null ? Ok(factory) : NotFound("Factory not found");
        }

        [HttpGet("factories")]
        public async Task<ActionResult<IEnumerable<FactoryUserDto>>> GetAllFactoryUsers()
        {
            var factories = await serviceManager.DashboardService.GetAllFactoryUsersAsync();
            return Ok(factories);
        }



        [HttpPost("add-agent")]
        public async Task<ActionResult<UserResultDTO>> AddAgent(RegisterDTO register)
            => Ok(await serviceManager.AuthenticationService.RegisterAsync(register));

        [HttpPut("update-agent/{agentId}")]
        public async Task<ActionResult> UpdateAgent(UpdateAgentDto updateAgentDto, string agentId)
        {
            await serviceManager.DashboardService.UpdateAgentAsync(updateAgentDto, agentId);
            return Ok("Environmental Agent Updated Successfully");
        }

        [HttpDelete("delete-agent/{agentId}")]
        public async Task<ActionResult<UserResultDTO>> DeleteAgent(string agentId)
        {
            await serviceManager.DashboardService.DeleteAgentAsync(agentId);
            return Ok();
        }
        [HttpGet("agents/{agentId}")]
        public async Task<ActionResult<EnvironmentalAgent>> GetAgentById(string agentId)
        {
            var agent = await serviceManager.DashboardService.GetAgentByIdAsync(agentId);
            return agent is not null ? Ok(agent) : NotFound("Agent not found");
        }

        [HttpGet("agents")]
        public async Task<ActionResult<IEnumerable<EnvironmentalAgent>>> GetAllAgentUsers()
        {
            var agents = await serviceManager.DashboardService.GetAllAgentUsersAsync();
            return Ok(agents);
        }
    }
}
