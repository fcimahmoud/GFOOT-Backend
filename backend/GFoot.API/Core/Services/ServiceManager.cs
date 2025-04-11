
namespace Services
{
    public class ServiceManager(
        UserManager<ApplicationUser> userManager,
        IOptions<JwtOptions> options,
        IUnitOfWork unitOfWork,
        IOptions<EmailSettings> emailSettings,
        IEmailService emailService,
        HttpClient httpClient,
        ILogger<CalculationsService> logger,
        IAuthenticationService authenticationService
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

        #region Dashboard Services

        private readonly Lazy<IDashboardService> _lazyDashboardService =
            new(() => new DashboardService(unitOfWork, userManager, authenticationService));

        public IDashboardService DashboardService => _lazyDashboardService.Value;

        #endregion

        #region Individuals Services

        private readonly Lazy<ICalculationsService> _lazyCalculationsService =
            new(() => new CalculationsService(unitOfWork, httpClient, logger));

        private readonly Lazy<IRankService> _lazyRankService =
            new(() => new RankService(unitOfWork));

        private readonly Lazy<IRecommendationService> _lazyRecommendationService =
            new(() => new RecommendationService(unitOfWork, httpClient));

        private readonly Lazy<IVisualizationService> _lazyVisualizationService =
            new(() => new VisualizationService(unitOfWork));

        public ICalculationsService CalculationsService => _lazyCalculationsService.Value;
        public IRankService RankService => _lazyRankService.Value;
        public IRecommendationService RecommendationService => _lazyRecommendationService.Value;
        public IVisualizationService VisualizationService => _lazyVisualizationService.Value;

        #endregion

        #region Factory Services

        #endregion

        #region Regulators Services

        #endregion
    }
}
