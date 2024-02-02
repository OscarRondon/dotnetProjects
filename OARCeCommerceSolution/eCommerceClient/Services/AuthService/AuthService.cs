﻿
using System.Net.Http.Json;

namespace eCommerceClient.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly ClientAppSettings _settings;
        private readonly HttpClient _httpClient;

        public AuthService(ClientAppSettings settings, HttpClient httpClient)
        {
            _settings = settings;
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse<int>> RegisterAsync(UserRegister request)
        {
            var result = await _httpClient.PostAsJsonAsync(_settings.BackendApiURL + "Auth/Register", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }

        public async Task<ServiceResponse<string>> LoginAsync(UserLogin request)
        {
            var result = await _httpClient.PostAsJsonAsync(_settings.BackendApiURL + "Auth/Login", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task<ServiceResponse<bool>> ChangePasswordAsync(UserChangePassword request)
        {
            var result = await _httpClient.PostAsJsonAsync(_settings.BackendApiURL + "Auth/Change-Password", request.Password);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }
    }
}