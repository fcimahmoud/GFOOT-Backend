using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace Services
{
    public class ServiceManager(
        UserManager<ApplicationUser> userManager,
        IOptions<JwtOptions> options,
        IUnitOfWork unitOfWork,
        IOptions<EmailSettings> emailSettings,
        IEmailService emailService,
        HttpClient httpClient,
        ILogger<CalculationsService> logger
        )
        : IServiceManager
    {
        #region Authentication Services

        private readonly Lazy<IAuthenticationService> _lazyAuthenticationService =
            new(() => new AuthenticationService(userManager, unitOfWork, options, emailService));
        private readonly Lazy<IEmailService> _lazyEmailService =
            new(() => new EmailService(emailSettings));

        public IAuthenticationService AuthenticationService => _lazyAuthenticationService.Value;
        public IEmailService EmailService => _lazyEmailService.Value;

        #endregion

        #region Individuals Services

        private readonly Lazy<ICalculationsService> _lazyCalculationsService =
            new(() => new CalculationsService(unitOfWork, httpClient, logger));

        private readonly Lazy<IRankService> _lazyRankService =
            new(() => new RankService(unitOfWork));

        public ICalculationsService CalculationsService => _lazyCalculationsService.Value;

        public IRankService RankService => _lazyRankService.Value;

        #endregion

        #region Factory Services

        #endregion

        #region Regulators Services

        #endregion
    }
}
