
using Mailjet.Client;
using Newtonsoft.Json.Linq;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Mailjet.Client.TransactionalEmails;

namespace PortafolioClient.Services.MailService
{
    public class MailService : IMailService
    {
        public async Task SentMailAsync(SendMail sendMail)
        {
            try
            {

                MailjetClient client = new MailjetClient("3a3695d7fa63d3f1d8b1d2fd10b0ea19", "5a68b633bf98f1a5e2ac722b3d4d7a46");
                /*
                MailjetRequest request = new MailjetRequest
                {
                    Resource = Send.Resource,
                }
                   .Property(Send.FromEmail, sendMail.SenderEmail)
                   .Property(Send.FromName, sendMail.SenderName)
                   .Property(Send.Subject, sendMail.Subject)
                   .Property(Send.TextPart, sendMail.Message)
                   .Property(Send.HtmlPart, sendMail.Message)
                   .Property(Send.Recipients, new JArray {
                new JObject {
                 {"Email", "oscar.rondon.c@gmail.com"}
                 }
                       });
                MailjetResponse response = await client.PostAsync(request);
                */
                MailjetRequest request = new MailjetRequest
                {
                    Resource = Send.Resource
                };

                var email = new TransactionalEmailBuilder()
                .WithFrom(new SendContact("from@test.com"))
                .WithSubject("Test subject")
                .WithHtmlPart("<h1>Header</h1>")
                .WithTo(new SendContact("oscar.rondon.c@gmail.com"))
                .Build();

                // invoke API to send email
                var response = await client.SendTransactionalEmailAsync(email);

                /*
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
                    Console.WriteLine(response.GetData());
                }
                else
                {
                    Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));
                    Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
                    Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
                }
                */
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
