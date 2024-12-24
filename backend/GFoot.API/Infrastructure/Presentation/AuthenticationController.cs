
namespace Presentation
{
    public class AuthenticationController (IServiceManager serviceManager)
        : ApiController
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserResultDTO>> Login(LoginDTO login)
            => Ok(await serviceManager.AuthenticationService.LoginAsync(login));

        [HttpPost("Register")]
        public async Task<ActionResult<UserResultDTO>> Register(RegisterDTO register)
            => Ok(await serviceManager.AuthenticationService.RegisterAsync(register));
    }
}
