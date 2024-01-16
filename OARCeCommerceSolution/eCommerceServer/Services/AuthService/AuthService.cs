
namespace eCommerceServer.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;

        public AuthService(DataContext context) 
        {
            _context = context;
        }
        public Task<ServiceResponse<int>> RegisterAsync(User user, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UserExistAsync(string email)
        {
            if(await _context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower()))
            return true;
            return false;
        }
    }
}
