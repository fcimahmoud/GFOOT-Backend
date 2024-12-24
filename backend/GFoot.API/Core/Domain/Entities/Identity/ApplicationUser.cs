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

        // Navigational Properties
        public virtual IndividualUser? IndividualUser { get; set; }
        public virtual FactoryUser? FactoryUser { get; set; }
        public virtual EnvironmentalAgent? EnvironmentalAgent { get; set; }
    }
}
