namespace eCommerceClient.Services.CartService
{
    public interface ICartService
    {
        public event Action OnChange;
        public Task AddToCartAsync(CartItem carItem);
        public Task<List<CartItem>> GetCartItemsAsync();
        public Task RemoveFromCartAsync(CartItem carItem);
    }
}
