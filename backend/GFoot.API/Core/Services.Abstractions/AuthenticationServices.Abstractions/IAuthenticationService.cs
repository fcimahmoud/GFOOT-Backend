global using Shared.AuthenticationModels;

namespace Services.Abstractions.AuthenticationServices.Abstractions
{
    public interface IAuthenticationService
    {
        public Task<UserResultDTO> LoginAsync(LoginDTO loginModel);
        public Task<UserResultDTO> RegisterAsync(RegisterDTO registerModel);
        public Task<bool> ForgotPasswordAsync(ForgotPasswordRequestDto model);
        public Task<bool> ResetPasswordAsync(ResetPasswordRequestDto model);
    }
}
