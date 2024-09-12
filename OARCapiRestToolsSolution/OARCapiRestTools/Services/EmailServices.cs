namespace OARCapiRestTools.Services
{
    public class EmailServices : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailServices(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        private async Task<ServiceResponse<string>> SendEmailSmtpTls(Email email, EmailConfiguration emailConfig)
        {
            var response = new ServiceResponse<string>()
            {
                Data = "",
                Success = false,
                Message = "Error sending email"

            };

            var mimeEmail = new MimeMessage();
            mimeEmail.From.Add(MailboxAddress.Parse(email.From));
            mimeEmail.To.Add(MailboxAddress.Parse(email.To));
            mimeEmail.Subject = email.Subject;
            mimeEmail.Body = new TextPart(TextFormat.Text)
            {
                Text = email.Body
            };

            using var smtp = new SmtpClient();
            smtp.Connect(emailConfig.smtpServer, emailConfig.smtpPort, SecureSocketOptions.StartTls);
            smtp.Authenticate(emailConfig.smtpUser, emailConfig.smtpPassword);
            response.Data = smtp.Send(mimeEmail);
            smtp.Disconnect(true);

            if (response.Data.Contains("2.0.0"))
            {
                response.Success = true;
                response.Message = "Ok";
            }

            return response;
        }

        public async Task<ServiceResponse<string>> SendEmailFromOARCDeveloperGmailCom(Email email)
        {
            EmailConfiguration emailConfig = new EmailConfiguration()
            {
                smtpServer = _configuration.GetSection("SmtpServers:S1:Host").Value,
                smtpPort = int.Parse(_configuration.GetSection("SmtpServers:S1:Port").Value),
                smtpUser = _configuration.GetSection("SmtpServers:S1:User").Value,
                smtpPassword = _configuration.GetSection("SmtpServers:S1:Password").Value
            };

            return await SendEmailSmtpTls(email, emailConfig);
        }
    }
}
