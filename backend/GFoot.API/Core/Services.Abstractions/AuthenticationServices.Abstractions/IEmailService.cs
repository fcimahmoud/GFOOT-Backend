namespace Services.Abstractions.AuthenticationServices.Abstractions
{
    public interface IEmailService
    {
        public Task<bool> SendEmailAsync(string to, string subject, string body, bool isHtml = true);
    }
}
