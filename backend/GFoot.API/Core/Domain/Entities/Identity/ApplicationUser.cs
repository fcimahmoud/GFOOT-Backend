global using Domain.Entities.Factory;
global using Domain.Entities.Individual;
global using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public required string DisplayName { get; set; }
        public required string UserType { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }

        // Add these fields for OTP verification
        public string? EmailConfirmationOTP { get; set; }
        public DateTime? OTPExpiryTime { get; set; }

        // Add OTP fields for password reset
        public string? PasswordResetOTP { get; set; }
        public DateTime? PasswordResetOTPExpiry { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

        // Navigational Properties
        public virtual IndividualUser? IndividualUser { get; set; }
        public virtual FactoryUser? FactoryUser { get; set; }
        public virtual EnvironmentalAgent? EnvironmentalAgent { get; set; }
    }
}
