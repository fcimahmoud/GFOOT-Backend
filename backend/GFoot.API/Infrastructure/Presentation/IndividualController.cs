
namespace Presentation.Individual_Controllers
{
    [Authorize(Roles = "IndividualUserRole")]
    public class IndividualController (IServiceManager serviceManager)
        : ApiController
    {
        [HttpPost("log-activity")]
        public async Task<IActionResult> LogActivity([FromQuery] ActivityDTO activity)
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

        [HttpGet("rank")]
        public async Task<IActionResult> GetUserRank()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { StatusCode = 401, ErrorMessage = "User not found." });

            var cityRank = await serviceManager.RankService.GetCityRankAsync(userId);
            var countryRank = await serviceManager.RankService.GetCountryRankAsync(userId);
            var globalRank = await serviceManager.RankService.GetGlobalRankAsync(userId);

            return Ok(new
            {
                CityRank = cityRank,
                CountryRank = countryRank,
                GlobalRank = globalRank
            });
        }
    }
}
