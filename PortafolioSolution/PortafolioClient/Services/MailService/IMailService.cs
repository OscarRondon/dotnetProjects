namespace PortafolioClient.Services.MailService
{
    public interface IMailService
    {
        public Task SentMailAsync(SendMail sendMail);
    }
}
