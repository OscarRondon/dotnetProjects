namespace eCommerceServer.Services.CartService
{
    public interface ICartService
    {
        public Task<ServiceResponse<List<CartProductResponse>>> GetCartProductsAsync(List<CartItem> cartItems);
        public Task<ServiceResponse<List<CartProductResponse>>> StoreCartItemsAsync(List<CartItem> cartItems/*, int userId*/);
    }
}
