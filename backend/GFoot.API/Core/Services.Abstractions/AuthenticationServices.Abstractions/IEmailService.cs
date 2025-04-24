namespace Services.Abstractions.AuthenticationServices.Abstractions
{
    public interface IEmailService
    {
        public Task<bool> SendEmailAsync(string to, string subject, string body, bool isHtml = true);

        Task<bool> SendEmailWithAttachmentAsync(string to, string subject, string body, byte[] attachment, string attachmentName, bool isHtml = true);

    }
}
