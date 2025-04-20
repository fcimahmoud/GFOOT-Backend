
global using Services.Abstractions.AuthenticationServices.Abstractions;
global using Services.Abstraction.Individual_Services;
global using Services.Abstractions.Individual_Services.Abstraction;
global using Services.Abstractions.Dashboard_Services.Abstraction;
global using Services.Abstractions.Factory_Services.Abstraction;

namespace Services.Abstractions
{
    public interface IServiceManager
    {
        public IAuthenticationService AuthenticationService { get; }
        public IEmailService EmailService { get; }
        public ICalculationsService CalculationsService { get; }
        public IRankService RankService { get; }
        public IRecommendationService RecommendationService { get; }
        public IDashboardService DashboardService { get; }
        public IVisualizationService VisualizationService { get; }

        // Factory Services
        public IFactoryProfile FactoryProfileService { get; }
        public IFactoryCalculationsService FactoryCalculationsService { get; }
        public IFactoryRecommendationService FactoryRecommendationService { get; }
        public IFactoryVisualizationService FactoryVisualizationService { get; }
    }
}
