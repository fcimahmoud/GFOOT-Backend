
global using Services.Abstractions.AuthenticationServices.Abstractions;
global using Services.Abstraction.Individual_Services;

namespace Services.Abstractions
{
    public interface IServiceManager
    {
        public IAuthenticationService AuthenticationService { get; }
        public IEmailService EmailService { get; }
        public ICalculationsService CalculationsService { get; }
    }
}
