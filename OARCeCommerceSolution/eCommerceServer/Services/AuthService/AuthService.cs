
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace eCommerceServer.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(
            DataContext context, 
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<int>> RegisterAsync(User user, string password)
        {
            if (await UserExistAsync(user.Email))
            {
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "User already exist"
                };
            }
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new ServiceResponse<int>
            {
                Success = true,
                Message = "Registration succesfully",
                Data = user.Id
            };
        }

        public async Task<bool> UserExistAsync(string email)
        {
            if (await _context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower()))
                return true;
            return false;
        }

        public async Task<ServiceResponse<string>> LoginAsync(string email, string password)
        {
            var response = new ServiceResponse<string>();

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());

            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found";
            }
            else
            {
                if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                {
                    response.Success = false;
                    response.Message = "Incorrect password";
                }
                else
                {
                    response.Success = true;
                    response.Message = "Login Successfull";
                    response.Data = CreateToken(user);

                }
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> ChangePasswordAsync(int userId, string newPassword)
        {
            var response = new ServiceResponse<bool>();
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found";
                response.Data = false;
            }
            else
            {
                CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                await _context.SaveChangesAsync();
                response.Success = true;
                response.Message = "Password updated successfully";
                response.Data = true;
            }

            return response;

        }

        private void CreatePasswordHash(string password, out byte[] passwprdHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwprdHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(GetToken()));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(1440),
                signingCredentials: credentials
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private string GetToken()
        {
            // Get values from .env file
            string token = _configuration.GetSection("AuthToken").Value;
            return token;
        }
        public int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        public string GetUserEmail() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
        }
    }
}
