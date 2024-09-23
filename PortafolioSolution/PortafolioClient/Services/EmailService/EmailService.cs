
using System.Net.Http.Json;
using System.Net;
using System.Runtime;

namespace PortafolioClient.Services.MailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public EmailService(
            IConfiguration configuration,
            HttpClient httpClient

            ) 
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse<string>> SentMailAsync(SendEmail sendMail)
        {
            string endpointUrl = _configuration.GetSection("Endpoints:OARCapiRestTools:Url").Value;
            Email email = new Email()
            {
                From = sendMail.SenderEmail,
                To = "oarc.developer@gmail.com",
                Subject = sendMail.Subject,
                Body = sendMail.SenderName + "\n" + sendMail.Message
            };
            var response = await _httpClient.PostAsJsonAsync("https://localhost:44346" + "/api/Email/oarcdeveloper", email);

            return await response.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }
    }
}
