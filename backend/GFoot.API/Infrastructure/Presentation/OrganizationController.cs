using Shared.FactoryModels;

namespace Presentation
{
    [Authorize(Roles = "FactoryUserRole")]
    public class OrganizationController(IServiceManager serviceManager) : ApiController
    {
        [HttpGet("profile")]
        public async Task<IActionResult> GetOrganizationProfile()
        {
            var organizationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(organizationUserId))
                return Unauthorized(new { StatusCode = 401, ErrorMessage = "User not found." });

            var profile = await serviceManager.FactoryProfileService.GetFactoryProfileAsync(organizationUserId);
            return Ok(profile);
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateOrganizationProfile(UpdateFactoryProfileDTO profile)
        {
            var organizationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(organizationUserId))
                return Unauthorized(new { StatusCode = 401, ErrorMessage = "User not found." });

            await serviceManager.FactoryProfileService.UpdateFactoryProfileAsync(organizationUserId, profile);

            return Ok("Factory Profile Updated Successfully!");
        }

        [HttpPost("calculation")]
        public async Task<IActionResult> CreateCalculation([FromBody] FactoryEmissionDTO factoryEmission)
        {
            var organizationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (organizationUserId == null) return Unauthorized(new
            {
                StatusCode = 401,
                ErrorMessage = "User not authenticated."
            });

            var result = await serviceManager.FactoryCalculationsService.LogCarbonFootPrintAsync(organizationUserId, factoryEmission);
            if (result == null) return BadRequest(new
            {
                StatusCode = 400,
                ErrorMessage = "Failed to Calculate Carbon FootPrint Organization for the . Please check your input data."
            });

            await serviceManager.FactoryRecommendationService.CreateRecommendationAsync(organizationUserId, factoryEmission, result.CarbonEmission);

            return Ok(new
            {
                Message = "Carbon FootPrint Calculated successfully."
            });
        }

        [HttpGet("footprint")]
        public async Task<IActionResult> GetFootPrint()
        {
            var organizationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (organizationUserId == null) return Unauthorized(new
            {
                StatusCode = 401,
                ErrorMessage = "User not authenticated."
            });

            return Ok(await serviceManager.FactoryCalculationsService.GetFootPrintAsync(organizationUserId));
        }

        [HttpGet("recommendations")]
        public async Task<IActionResult> GetOrganizationRecommendations()
        {
            var organizationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(organizationUserId))
                return Unauthorized(new { StatusCode = 401, ErrorMessage = "User not found." });

            var recommendations = await serviceManager.FactoryRecommendationService.GetAllRecommendationsAsync(organizationUserId);
            return Ok(recommendations);
        }

        [HttpGet("recommendations/{recId}")]
        public async Task<IActionResult> GetOrganizationRecommendation(string recId)
        {
            var organizationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(organizationUserId))
                return Unauthorized(new { StatusCode = 401, ErrorMessage = "User not found." });

            var recommendation = await serviceManager.FactoryRecommendationService.GetRecommendationAsync(recId);
            return Ok(recommendation);
        }

        [HttpGet("Visualization")]
        public async Task<IActionResult> GetOrganizationVisualizationData(bool ascending)
        {
            var organizationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(organizationUserId))
                return Unauthorized(new { StatusCode = 401, ErrorMessage = "User not found." });

            var dataMonthly = await serviceManager.FactoryVisualizationService.GetVisualizedDataMonthlyAsync(organizationUserId, ascending);
            var dataYearly = await serviceManager.FactoryVisualizationService.GetVisualizedDataYearlyAsync(organizationUserId, ascending);

            return Ok(new
            {
                DataMonthly = dataMonthly,
                DataYearly = dataYearly
            });
        }
    }
}