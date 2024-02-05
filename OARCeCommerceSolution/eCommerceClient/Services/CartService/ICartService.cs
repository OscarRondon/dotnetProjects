namespace eCommerceClient.Services.CartService
{
    public interface ICartService
    {
        public event Action OnChange;
        public Task AddToCartAsync(CartItem carItem);
        public Task<List<CartItem>> GetCartItemsAsync();
        public Task RemoveFromCartAsync(int productId, int productTypeId);
        public Task<List<CartProductResponse>> GetCartProductsAsync();
        public Task UpdateQuantityAsync(CartProductResponse product);
        public Task StoreCartItemsAsync(bool emptyLocalCart);
        public Task GetCartItemsCountAsync();
    }
}
