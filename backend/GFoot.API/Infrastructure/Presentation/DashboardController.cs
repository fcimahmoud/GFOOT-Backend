
using Shared.DashboardModels;

namespace Presentation
{
    public class DashboardController(IServiceManager serviceManager)
        : ApiController
    {
        [HttpPost("add-factory")]
        public async Task<ActionResult<UserResultDTO>> AddFactory(RegisterDTO register)
            => Ok(await serviceManager.AuthenticationService.RegisterAsync(register));

        [HttpPut("update-factory/{factoryId}")]
        public async Task<ActionResult> UpdateFactory(UpdateFactoryDto updateFactoryDto, string agentId)
        {
            await serviceManager.DashboardService.UpdateFactoryAsync(updateFactoryDto, agentId);
            return Ok("Factory Updated Successfully");
        }

        [HttpDelete("delete-factory/{factoryId}")]
        public async Task<ActionResult<UserResultDTO>> DeleteFactory(string factoryId)
        {
            await serviceManager.DashboardService.DeleteFactoryAsync(factoryId);
            return Ok();
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
    }
}
