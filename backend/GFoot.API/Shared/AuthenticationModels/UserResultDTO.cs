namespace Shared.AuthenticationModels
{
    public record UserResultDTO(string Id, string DisplayName, string UserType, string Email, string AccessToken,
    string RefreshToken);
}
