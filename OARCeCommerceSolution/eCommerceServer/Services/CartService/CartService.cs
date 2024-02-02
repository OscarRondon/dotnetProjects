
using System.Security.Claims;

namespace eCommerceServer.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartService(DataContext context, IHttpContextAccessor httpContextAccessor) 
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<List<CartProductResponse>>> GetCartProductsAsync(List<CartItem> cartItems)
        {
            var result = new ServiceResponse<List<CartProductResponse>>
            {
                Data = new List<CartProductResponse>()
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

                var cartProductResponse = new CartProductResponse
                {
                    ProductId = product.Id,
                    Tittle = product.Title,
                    ImageUrl = product.ImageUrl,
                    Price = productVariant.Price,
                    ProductTypeId = productVariant.ProductTypeId,
                    ProductType = productVariant.ProductType.Name,
                    Quantity = item.Quantity,
                    
                };

                result.Data.Add(cartProductResponse);
            }

            return result;
        }

        public async Task<ServiceResponse<List<CartProductResponse>>> StoreCartItemsAsync(List<CartItem> cartItems/*, int userId*/)
        {

            cartItems.ForEach(ci => ci.UserId = GetUserId() /*userId*/);
            await _context.CarItems.AddRangeAsync(cartItems);
            await _context.SaveChangesAsync();

            return await GetCartProductsAsync(await _context.CarItems.Where(ci => ci.UserId == GetUserId() /*userId*/).ToListAsync());

        }
    }
}
