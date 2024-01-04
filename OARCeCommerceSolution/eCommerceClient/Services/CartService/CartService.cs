
using Blazored.LocalStorage;
using eCommerceShared;
using System.Net.Http.Json;

namespace eCommerceClient.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _httpClient;
        private readonly ClientAppSettings _settings;

        public CartService(ILocalStorageService localStorage, HttpClient httpClient, ClientAppSettings settings) 
        {
            _localStorage = localStorage;
            _httpClient = httpClient;
            _settings = settings;
        }

        public event Action OnChange;

        public async Task AddToCartAsync(CartItem carItem)
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (cart == null)
            {
                cart = new List<CartItem>();
            }
            cart.Add(carItem);
            await _localStorage.SetItemAsync("cart", cart);
            OnChange?.Invoke();
        }

        public async Task<List<CartItem>> GetCartItemsAsync()
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (cart == null)
            {
                cart = new List<CartItem>();
            }
            return cart;
        }

        public async Task<List<CartProductReponse>> GetCartProductsAsync()
        {
            var carItems = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            var response = await _httpClient.PostAsJsonAsync(_settings.BackendApiURL + "Cart/Products", carItems);
            var carProducts = await response.Content.ReadFromJsonAsync<List<CartProductReponse>>();
            return carProducts;
        }

        public Task RemoveFromCartAsync(CartItem carItem)
        {
            throw new NotImplementedException();
        }
    }
}
