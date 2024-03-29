﻿namespace eCommerceServer.Services.AuthService
{
    public interface IAuthService
    {
        public Task<ServiceResponse<int>> RegisterAsync(User user, string password);
        public Task<bool> UserExistAsync(string email);
        public Task<ServiceResponse<string>> LoginAsync(string email, string password);
        public Task<ServiceResponse<bool>> ChangePasswordAsync(int userId,  string newPassword);
        public int GetUserId();
        public string GetUserEmail();
        public Task<User> GetUserByEmailAsync(string email);
    }
}
