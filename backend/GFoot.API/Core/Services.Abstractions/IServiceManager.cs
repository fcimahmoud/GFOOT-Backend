
global using Services.Abstractions.AuthenticationServices.Abstractions;
global using Services.Abstraction.Individual_Services;
global using Services.Abstractions.Individual_Services.Abstraction;

namespace Services.Abstractions
{
    public interface IServiceManager
    {
        public IAuthenticationService AuthenticationService { get; }
        public IEmailService EmailService { get; }
        public ICalculationsService CalculationsService { get; }
        public IRankService RankService { get; }
    }
}
