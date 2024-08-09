
using Mailjet.Client;
using Newtonsoft.Json.Linq;
using Mailjet.Client;
using Mailjet.Client.Resources;

namespace PortafolioClient.Services.MailService
{
    public class MailService : IMailService
    {
        public async Task SentMailAsync()
        {
            throw new NotImplementedException();

            MailjetClient client = new MailjetClient(Environment.GetEnvironmentVariable("MJ_APIKEY_PUBLIC"), Environment.GetEnvironmentVariable("MJ_APIKEY_PRIVATE"));
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
               .Property(Send.FromEmail, "pilot@mailjet.com")
               .Property(Send.FromName, "Mailjet Pilot")
               .Property(Send.Subject, "Your email flight plan!")
               .Property(Send.TextPart, "Dear passenger, welcome to Mailjet! May the delivery force be with you!")
               .Property(Send.HtmlPart, "<h3>Dear passenger, welcome to <a href=\"https://www.mailjet.com/\">Mailjet</a>!<br />May the delivery force be with you!")
               .Property(Send.Recipients, new JArray {
                new JObject {
                 {"Email", "passenger@mailjet.com"}
                 }
                   });
            MailjetResponse response = await client.PostAsync(request);
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
        }
    }
}
