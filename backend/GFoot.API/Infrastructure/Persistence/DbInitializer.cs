
namespace Persistence
{
    public class DbInitializer (
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager
        )
        : IDbInitializer
    {

        public async Task InitializeIdentityAsync()
        {
            var roles = new[]
            { "Admin", "IndividualUserRole", "FactoryUserRole", "EnvironmentalAgentRole" };

            // Seed Default Roles
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Seed Default Users
            if (!userManager.Users.Any())
            {
                var admin = new ApplicationUser
                {
                    DisplayName = "GFoot Admin",
                    Email = "ma5740@fayoum.edu.eg",
                    UserName = "GFootAdmin",
                    UserType = "Admin"
                };

                await userManager.CreateAsync(admin, "P@ssw0rd");
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
