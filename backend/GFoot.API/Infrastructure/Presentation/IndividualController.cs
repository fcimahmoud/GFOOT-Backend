
namespace Presentation.Individual_Controllers
{
    
    public class IndividualController (IServiceManager serviceManager)
        : ApiController
    {
        [HttpPost("log-activity")]
        [Authorize(Roles = "IndividualUserRole")]
        public async Task<IActionResult> LogActivity([FromBody] ActivityDTO activity)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized(new {
                StatusCode = 401,
                ErrorMessage = "User not authenticated."
            });

            var result = await serviceManager.CalculationsService.LogActivityAsync(userId, activity);
            if (result == null) return BadRequest(new {
                StatusCode = 400,
                ErrorMessage = "Failed to log activity. Please check your input data."
            });

            return Ok(new {
                Message = "Activity logged successfully.",
                CarbonEmission = result.CarbonEmission
            });
        }
    }
}
