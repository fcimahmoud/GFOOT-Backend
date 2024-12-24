
namespace Services
{
    public class ServiceManager(
        UserManager<ApplicationUser> userManager,
        IOptions<JwtOptions> options,
        IUnitOfWork unitOfWork
        )
        : IServiceManager
    {
        private readonly Lazy<IAuthenticationService> _lazyAuthenticationService =
            new(() => new AuthenticationService(userManager, unitOfWork, options));
        public IAuthenticationService AuthenticationService => _lazyAuthenticationService.Value;
    }
}
