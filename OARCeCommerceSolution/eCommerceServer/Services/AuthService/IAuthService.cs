namespace eCommerceServer.Services.AuthService
{
    public interface IAuthService
    {
        public Task<ServiceResponse<int>> RegisterAsync(User user, string password);
        public Task<bool> UserExistAsync(string email);
        public Task<ServiceResponse<string>> LoginAsync(string email, string password);
    }
}
