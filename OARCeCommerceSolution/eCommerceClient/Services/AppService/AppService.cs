
using eCommerceClient.Pages;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace eCommerceClient.Services.AppService
{
    public class AppService : IAppService
    {
        private readonly HttpClient _httpClient;

        public string ApiUrl { get; set; } = string.Empty;

        public AppService(HttpClient httpClient) 
        {
            _httpClient = httpClient;
            GetEndpointUrls();
        }

        public async Task GetEndpointUrls()
        {
            // Get values from .env file
            var result = await _httpClient.GetFromJsonAsync<object>("Configs/env.json");
        }
    }
}
