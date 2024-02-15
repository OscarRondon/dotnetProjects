namespace eCommerceServer.Services.CartService
{
    public interface ICartService
    {
        public Task<ServiceResponse<List<CartProductResponse>>> GetCartProductsAsync(List<CartItem> cartItems);
        public Task<ServiceResponse<List<CartProductResponse>>> StoreCartItemsAsync(List<CartItem> cartItems/*, int userId*/);
        public Task<ServiceResponse<int>> GetCartItemsCountAsync();
        public Task<ServiceResponse<List<CartProductResponse>>> GetDBCartProductsAsync(int? userId = null);
        public Task<ServiceResponse<bool>> AddToCartAsync(CartItem cartItem);
        public Task<ServiceResponse<bool>> UpdateQuantityAsync(CartItem cartItem);
        public Task<ServiceResponse<bool>> RemoveItemFromCartAsync(int productId, int productTypeId);
    }
}
