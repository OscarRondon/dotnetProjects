
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
    }
}
