using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace eCommerceClient.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        //private readonly AuthenticationStateProvider _authStateProvider;
        private readonly NavigationManager _navigationManager;
        private readonly ClientAppSettings _settings;
        private readonly IAuthService _authService;

        public OrderService(
            HttpClient httpClient, 
            NavigationManager navigationManager,
            ClientAppSettings settings,
            /*AuthenticationStateProvider authStateProvider,*/
            IAuthService authService
            ) 
        {
            _httpClient = httpClient;
            _settings = settings;
            _authService = authService;
            //_authStateProvider = authStateProvider;
            _navigationManager = navigationManager;
        }

        //It was move to the AuthService
        //public async Task<bool> IsUserAuthenticated() => (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;

        public async Task PlaceOrderAsync()
        {
            if ( await _authService.IsUserAuthenticated() ) 
            {
                await _httpClient.PostAsync(_settings.BackendApiURL + "Order/Add", null);
            }
            else
            {
                _navigationManager.NavigateTo("Login");
            }
        }

        public async Task<string> PlaceOrderURLReturnAsync()
        {
            string response = "";
            if (await _authService.IsUserAuthenticated())
            {
                var result = await _httpClient.PostAsync(_settings.BackendApiURL + "Payment/Checkout", null);
                response = await result.Content.ReadAsStringAsync();
            }
            else
            {
                response = "Login";
            }
            return response;
        }

        public async Task<List<OrderOverviewResponse>> GetOrderOverviewAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<OrderOverviewResponse>>>(_settings.BackendApiURL + "Order");
            return response.Data;
        }

        public async Task<OrderDetailsResponse> GetOrderDetailsAsync(int orderId)
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<OrderDetailsResponse>>(_settings.BackendApiURL + $"Order/{orderId}");
            return response.Data;
        }
    }
}
