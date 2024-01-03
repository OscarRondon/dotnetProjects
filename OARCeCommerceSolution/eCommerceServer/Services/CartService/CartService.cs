
namespace eCommerceServer.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly DataContext _context;

        public CartService(DataContext context) 
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<CartProductReponse>>> GetCartProductsAsync(List<CartItem> cartItems)
        {
            var result = new ServiceResponse<List<CartProductReponse>>
            {
                Data = new List<CartProductReponse>()
            };

            foreach (var item in cartItems) 
            {
                var product = _context.Products.Where(p => p.Id == item.ProductId).FirstOrDefault();
                if (product == null)
                    continue;

                var productVariant = await _context.ProductVariants
                    .Where(pv => pv.ProductId == item.ProductId && pv.ProductTypeId == item.ProductTypeId)
                    .Include(pt => pt.ProductType)
                    .FirstOrDefaultAsync();
                if (productVariant == null)
                    continue;

                var cartProductResponse = new CartProductReponse
                {
                    ProductId = product.Id,
                    Tittle = product.Title,
                    ImageUrl = product.ImageUrl,
                    Price = productVariant.Price,
                    ProductTypeId = productVariant.ProductTypeId,
                    ProductType = productVariant.ProductType.Name,
                    
                };

                result.Data.Add(cartProductResponse);
            }

            return result;
        }
    }
}
