using Microsoft.AspNetCore.Components;

namespace eCommerceClient.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly NavigationManager _navigationManager;
        private readonly ClientAppSettings _settings;

        public OrderService(
            HttpClient httpClient, 
            AuthenticationStateProvider authStateProvider,
            NavigationManager navigationManager,
            ClientAppSettings settings
            ) 
        {
            _httpClient = httpClient;
            _settings = settings;
            _authStateProvider = authStateProvider;
            _navigationManager = navigationManager;
        }

        private async Task<bool> IsUserAuthenticated() => (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;

        public async Task PlaceOrderAsync()
        {
            if ( await IsUserAuthenticated() ) 
            {
                await _httpClient.PostAsync(_settings.BackendApiURL + "Order/Add", null);
            }
            else
            {
                _navigationManager.NavigateTo("Login");
            }
        }

    }
}
