namespace eCommerceClient.Services.AuthService
{
    public interface IAuthService
    {
        public Task<ServiceResponse<int>> RegisterAsync(UserRegister user);
    }
}
