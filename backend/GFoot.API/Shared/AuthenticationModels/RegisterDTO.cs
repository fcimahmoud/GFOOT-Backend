namespace Shared.AuthenticationModels
{
    public record RegisterDTO
    {
        public string UserType { get; init; }
        public string DisplayName { get; init; }
        public string UserName { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public string? PhoneNumber { get; init; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? IndustryType { get; init; }
    }
}
