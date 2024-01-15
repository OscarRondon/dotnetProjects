
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
            var sameItem = cart.Find(item => item.ProductId == carItem.ProductId && item.ProductTypeId == carItem.ProductTypeId);
            if (sameItem == null)
            {
                cart.Add(carItem);
            }
            else
            {
                sameItem.Quantity += carItem.Quantity;
            }
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

        public async Task<List<CartProductResponse>> GetCartProductsAsync()
        {
            var carItems = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            var response = await _httpClient.PostAsJsonAsync(_settings.BackendApiURL + "Cart/Products", carItems);
            var carProducts = await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartProductResponse>>>();
            return carProducts.Data;
        }

        public async Task RemoveFromCartAsync(int productId, int productTypeId)
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (cart == null) return;
            var cartItem = cart.Where(item => item.ProductId == productId && item.ProductTypeId == productTypeId).FirstOrDefault();
            if (cartItem != null)
            {
                cart.Remove(cartItem);
                await _localStorage.SetItemAsync("cart", cart);
            }
            OnChange?.Invoke();
        }

        public async Task UpdateQuantityAsync(CartProductResponse product)
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (cart == null) return;
            var cartItem = cart.Find(item => item.ProductId == product.ProductId && item.ProductTypeId == product.ProductTypeId);
            if (cartItem != null)
            {
                cartItem.Quantity = product.Quantity;
                await _localStorage.SetItemAsync("cart", cart);
            }
        }
    }
}
