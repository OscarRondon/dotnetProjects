namespace eCommerceServer.Services.CartService
{
    public interface ICartService
    {
        public Task<ServiceResponse<List<CartProductResponse>>> GetCartProductsAsync(List<CartItem> cartItems);
    }
}
