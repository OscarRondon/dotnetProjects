
using System.Security.Cryptography;

namespace eCommerceServer.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;

        public AuthService(DataContext context) 
        {
            _context = context;
        }
        public async Task<ServiceResponse<int>> RegisterAsync(User user, string password)
        {
            if(await UserExistAsync(user.Email))
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
                Message = "User created",
                Data = user.Id
            };
        }

        public async Task<bool> UserExistAsync(string email)
        {
            if(await _context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower()))
            return true;
            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwprdHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwprdHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
