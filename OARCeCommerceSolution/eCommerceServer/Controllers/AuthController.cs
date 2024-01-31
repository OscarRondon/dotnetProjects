using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eCommerceServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) 
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> RegisterAsync(UserRegister request)
        {
            User newUser = new User();
            newUser.Email = request.Email;

            var response = await _authService.RegisterAsync(newUser, request.Password);

            if(!response.Success)
            {
                return BadRequest(response);
            }

            return Created("", response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<string>>> LoginAsync(UserLogin request)
        {
            var response = await _authService.LoginAsync(request.Email, request.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("Change-Password"), Authorize]
        public async Task<ActionResult<ServiceResponse<bool>>> PasswoerdChangeAsync([FromBody] string newPassword)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _authService.ChangePasswordAsync(int.Parse(userId), newPassword);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
