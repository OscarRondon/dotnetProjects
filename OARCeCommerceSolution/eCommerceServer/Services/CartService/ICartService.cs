namespace eCommerceServer.Services.CartService
{
    public interface ICartService
    {
        public Task<ServiceResponse<List<CartProductReponse>>> GetCartProductsAsync(List<CartItem> cartItems);
    }
}
