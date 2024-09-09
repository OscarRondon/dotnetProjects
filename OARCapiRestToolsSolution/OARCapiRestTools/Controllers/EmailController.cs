using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace OARCapiRestTools.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        [HttpPost("oarcdeveloper", Name = "oarc.developer@gmail.com")]
        public IActionResult SendEmailFromOARCDeveloperGmailCom()
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("tania76@ethereal.email"));
            email.To.Add(MailboxAddress.Parse("oscar.rondon.c@gmail.com"));
            email.Subject = "Email Title";
            email.Body = new TextPart(TextFormat.Text) {
                Text = "Email body content"
            };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("oarc.developer@gmail.com", "htoo yqds rojp gjta");
            smtp.Send(email);
            smtp.Disconnect(true);

            return Ok();
        }
    }
}
