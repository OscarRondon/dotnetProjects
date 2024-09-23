namespace PortafolioClient.Services.MailService
{
    public interface IEmailService
    {
        public Task<ServiceResponse<string>> SentMailAsync(SendEmail sendMail);
    }
}
