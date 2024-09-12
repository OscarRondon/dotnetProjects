using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OARCapiRestTools.Services;


namespace OARCapiRestTools.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService) 
        {
            _emailService = emailService;
        }

        [HttpPost("oarcdeveloper", Name = "oarc.developer@gmail.com")]
        public async Task<IActionResult> SendEmailFromOARCDeveloperGmailCom(Email email)
        {
            var response = await _emailService.SendEmailFromOARCDeveloperGmailCom(email);

            if (response.Success)
            {
                return Ok();
            }

            return BadRequest(response.Data);
        }
    }
}
