
using Shared.AgentModels;

namespace Presentation
{
    [Authorize(Roles = "EnvironmentalAgentRole")]
    public class EnvironmentalAgentController (IServiceManager serviceManager)
        : ApiController
    {
        [HttpGet("factories-carbon-footprint")]
        public async Task<IActionResult> GetAllFactoriesWithCarbonFootprints()
            => Ok(await serviceManager.EnvironmentalAgentService.GetAllFactoriesWithCarbonFootprintsAsync());

        [HttpGet("factories-exceeding-threshold")]
        public async Task<IActionResult> GetFactoriesExceedingThreshold()
            => Ok(await serviceManager.EnvironmentalAgentService.GetFactoriesExceedingCarbonThresholdAsync((float)2.5));

        [HttpPost("report/all/{factoryId}")]
        public async Task<IActionResult> CreateReportOfAll(string factoryId)
        {
            var agentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (agentId == null) return Unauthorized(new
            {
                StatusCode = 401,
                ErrorMessage = "User not authenticated."
            });
            var message = await serviceManager.EnvironmentalAgentService.CreateReportOfAllCalculationsAsync(factoryId, agentId);
            return Ok(message);
        }

        [HttpPost("report/latest/{factoryId}")]
        public async Task<IActionResult> CreateReportOfLatest(string factoryId)
        {
            var agentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (agentId == null) return Unauthorized(new
            {
                StatusCode = 401,
                ErrorMessage = "User not authenticated."
            });
            var message = await serviceManager.EnvironmentalAgentService.CreateReportOfLatestCalculationAsync(factoryId, agentId);
            return Ok(message);
        }

        [HttpGet("report/all-by-calculation-type")]
        public async Task<IActionResult> GetReportsByCalculationType([FromQuery] bool onLatest = true)
        {
            var agentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (agentId == null) return Unauthorized(new
            {
                StatusCode = 401,
                ErrorMessage = "User not authenticated."
            });
            var reports = await serviceManager.EnvironmentalAgentService.GetReportsByCalculationTypeAsync(agentId, onLatest);
            return Ok(reports);
        }

        [HttpGet("report/factory/{factoryId}")]
        public async Task<IActionResult> GetFactoryReports(string factoryId, [FromQuery] bool onLatest = true)
        {
            var agentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (agentId == null) return Unauthorized(new
            {
                StatusCode = 401,
                ErrorMessage = "User not authenticated."
            });

            var reports = await serviceManager.EnvironmentalAgentService.GetAllReportsOfFactoryAsync(agentId, factoryId, onLatest);
            return Ok(reports);
        }

        [HttpGet("report/{reportId}")]
        public async Task<ActionResult<ReportDto>> GetReportById(string reportId)
        {
            var report = await serviceManager.EnvironmentalAgentService.GetReportByIdAsync(reportId);
            return Ok(report);
        }

        [HttpPost("report/send/{reportId}")]
        public async Task<IActionResult> SendReportToAgent(string reportId)
        {   
            await serviceManager.EnvironmentalAgentService.SendReportToAgentAsync(reportId);
            return Ok("Report sent to agent email successfully.");
        }


        [HttpDelete("report/{reportId}")]
        public async Task<IActionResult> DeleteReport(string reportId)
        {
            await serviceManager.EnvironmentalAgentService.DeleteReportAsync(reportId);
            return Ok("Report deleted successfully");
        }

        //[HttpPost("report/send-as-pdf/{reportId}")]
        //public async Task<IActionResult> SendReportToAgentAsPdf(string reportId)
        //{
        //    await serviceManager.EnvironmentalAgentService.SendReportToAgentAsPdfAsync(reportId);
        //    return Ok("Report sent as PDF to agent email successfully.");
        //}

    }
}
