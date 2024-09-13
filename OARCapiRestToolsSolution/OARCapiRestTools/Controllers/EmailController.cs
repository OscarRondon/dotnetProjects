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
        public async Task<ActionResult<ServiceResponse<string>>> SendEmailFromOARCDeveloperGmailCom(Email email)
        {
            var response = await _emailService.SendEmailFromOARCDeveloperGmailCom(email);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
