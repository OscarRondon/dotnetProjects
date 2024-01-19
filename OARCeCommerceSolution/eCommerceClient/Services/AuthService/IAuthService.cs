namespace eCommerceClient.Services.AuthService
{
    public interface IAuthService
    {
        public Task<ServiceResponse<int>> RegisterAsync(UserRegister request);
        public Task<ServiceResponse<string>> LoginAsync(UserLogin request);
    }
}
