    
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

            await serviceManager.RecommendationService.CreateRecommendationAsync(userId, activity, result.CarbonEmission);


            return Ok(new {
                Message = "Activity logged successfully."
            });
        }
        [HttpGet("footprint")]
        public async Task<IActionResult> GetFootPrint()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized(new
            {
                StatusCode = 401,
                ErrorMessage = "User not authenticated."
            });

            return Ok(await serviceManager.CalculationsService.GetFootPrintAsync(userId));
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

        [HttpGet("recommendations")]
        public async Task<IActionResult> GetUserRecommendation()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { StatusCode = 401, ErrorMessage = "User not found." });

            var recommendations = await serviceManager.RecommendationService.GetAllRecommendationsAsync(userId);
            return Ok(recommendations);
        }

        [HttpGet("recommendations/{recId}")]
        public async Task<IActionResult> GetRecommendationById(string recId)
        {
            var recommendations = await serviceManager.RecommendationService.GetRecommendationAsync(recId);
            return Ok(recommendations);
        }

        [HttpGet("visualization")]
        public async Task<IActionResult> GetUserVisualizationData(bool ascending)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { StatusCode = 401, ErrorMessage = "User not found." });

            var dataDaily = await serviceManager.VisualizationService.GetVisualizedDataDailyAsync(userId, ascending);
            var dataMonthly = await serviceManager.VisualizationService.GetVisualizedDataMonthlyAsync(userId, ascending);
            var dataYearly = await serviceManager.VisualizationService.GetVisualizedDataYearlyAsync(userId, ascending);

            return Ok(new
            {
                DataDaily = dataDaily,
                DataMonthly = dataMonthly,
                DataYearly = dataYearly
            });
        }
    }
}
