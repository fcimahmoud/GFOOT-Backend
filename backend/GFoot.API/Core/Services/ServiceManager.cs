
global using AutoMapper;
global using Services.FactoryServices;
using Services.Abstractions.Agent_Services.Abstraction;
using Services.AgentServices;

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
        ILogger<FactoryCalculationsService> fLogger,
        IAuthenticationService authenticationService,
        IMapper mapper
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

        private readonly Lazy<IProfileService> _lazyProfileService =
            new(() => new ProfileService(unitOfWork, userManager));

        public IProfileService ProfileService => _lazyProfileService.Value;
        public ICalculationsService CalculationsService => _lazyCalculationsService.Value;
        public IRankService RankService => _lazyRankService.Value;
        public IRecommendationService RecommendationService => _lazyRecommendationService.Value;
        public IVisualizationService VisualizationService => _lazyVisualizationService.Value;

        #endregion

        #region Factory Services

        private readonly Lazy<IFactoryProfile> _lazyFactoryProfileService =
            new(() => new FactoryProfile(unitOfWork, mapper, userManager));
        private readonly Lazy<IFactoryCalculationsService> _lazyFactoryCalculationsService =
           new(() => new FactoryCalculationsService(unitOfWork, httpClient, fLogger, mapper));

        private readonly Lazy<IFactoryRecommendationService> _lazyFactoryRecommendationService =
            new(() => new FactoryRecommendationService(unitOfWork, httpClient));

        private readonly Lazy<IFactoryVisualizationService> _lazyFactoryVisualizationService =
            new(() => new FactoryVisualizationService(unitOfWork));

        private readonly Lazy<IGuestRequestService> _lazyGuestRequestService =
            new(() => new GuestRequestService(unitOfWork));

        public IFactoryProfile FactoryProfileService => _lazyFactoryProfileService.Value;
        public IFactoryCalculationsService FactoryCalculationsService => _lazyFactoryCalculationsService.Value;
        public IFactoryRecommendationService FactoryRecommendationService => _lazyFactoryRecommendationService.Value;
        public IFactoryVisualizationService FactoryVisualizationService => _lazyFactoryVisualizationService.Value;
        public IGuestRequestService GuestRequestService => _lazyGuestRequestService.Value;

        #endregion

        #region Regulators Services

        private readonly Lazy<IEnvironmentalAgentService> _lazyEnvironmentalAgentService =
            new(() => new EnvironmentalAgentService(unitOfWork, emailService));

        public IEnvironmentalAgentService EnvironmentalAgentService => _lazyEnvironmentalAgentService.Value;

        #endregion
    }
}
