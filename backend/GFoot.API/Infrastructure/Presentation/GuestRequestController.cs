
namespace Presentation
{
    public class GuestRequestController (IServiceManager serviceManager)
        : ApiController
    {
        // Public Endpoint for unregistered factories
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SubmitRequest([FromBody] CreateGuestServiceRequestDto dto)
        {
            await serviceManager.GuestRequestService.SubmitRequestAsync(dto);
            return Ok("Request submitted successfully. We'll get back to you soon.");
        }

        // Admin view of all guest requests
        [Authorize(Roles = "AdminRole")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GuestServiceRequestDto>>> GetAll()
            => Ok(await serviceManager.GuestRequestService.GetAllAsync());
    }
}
