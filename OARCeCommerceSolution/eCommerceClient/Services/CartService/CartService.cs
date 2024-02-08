
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
        private readonly AuthenticationStateProvider _authStateProvider;

        public CartService(
            ILocalStorageService localStorage,
            HttpClient httpClient,
            ClientAppSettings settings,
            AuthenticationStateProvider authStateProvider
            )
        {
            _localStorage = localStorage;
            _httpClient = httpClient;
            _settings = settings;
            _authStateProvider = authStateProvider;
        }

        public event Action OnChange;

        public async Task<bool> IsUserAuthenticated()
        {
            return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
        }

        public async Task AddToCartAsync(CartItem carItem)
        {
            if (await IsUserAuthenticated())
            {
                var response = await _httpClient.PostAsJsonAsync(_settings.BackendApiURL + "Cart/Add", carItem);

            }
            else
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
            }

            await GetCartItemsCountAsync();
        }

        //public async Task<List<CartItem>> GetCartItemsAsync()
        //{
        //    GetCartItemsCountAsync();
        //    var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
        //    if (cart == null)
        //    {
        //        cart = new List<CartItem>();
        //    }
        //    return cart;
        //}

        public async Task<List<CartProductResponse>> GetCartProductsAsync()
        {

            if (await IsUserAuthenticated())
            {
                var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<CartProductResponse>>>(_settings.BackendApiURL + "Cart");
                return response.Data;
            }
            else 
            {
                var carItems = await _localStorage.GetItemAsync<List<CartItem>>("cart");
                if(carItems == null)
                {
                    return new List<CartProductResponse>();
                }
                var response = await _httpClient.PostAsJsonAsync(_settings.BackendApiURL + "Cart/Products", carItems);
                var carProducts = await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartProductResponse>>>();
                return carProducts.Data;
            }
            
            
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
                GetCartItemsCountAsync();
            }
        }

        public async Task StoreCartItemsAsync(bool emptyLocalCart = false)
        {
            var localCart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            
            if(localCart == null) return;

            var response = await _httpClient.PostAsJsonAsync(_settings.BackendApiURL + "Cart", localCart);

            if (emptyLocalCart) 
            {
                await _localStorage.RemoveItemAsync("cart");
            }
        }

        public async Task UpdateQuantityAsync(CartProductResponse product)
        {
            if (await IsUserAuthenticated())
            {
                var request = new CartItem
                {
                    ProductId = product.ProductId,
                    ProductTypeId = product.ProductTypeId,
                    Quantity = product.Quantity,
                };
                await _httpClient.PutAsJsonAsync(_settings.BackendApiURL + "Cart/Update-Quantity", request);
            }
            else 
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

        public async Task GetCartItemsCountAsync()
        {
            int result = 0;
            if (await IsUserAuthenticated())
            {
                var response = await _httpClient.GetFromJsonAsync<ServiceResponse<int>>(_settings.BackendApiURL + "Cart/CartItems-Count");
                result = response.Data;
            }
            else
            {
                var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
                if (cart != null)
                {
                    result = cart.Count;
                }
            }
            await _localStorage.SetItemAsync<int>("carItemsCount", result);
            OnChange?.Invoke();
        }
    }
}
