
using Blazored.LocalStorage;
using eCommerceShared;

namespace eCommerceClient.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorage;

        public CartService(ILocalStorageService localStorage) 
        {
            _localStorage = localStorage;
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

        public Task RemoveFromCartAsync(CartItem carItem)
        {
            throw new NotImplementedException();
        }
    }
}
