﻿//using System.Security.Claims;

namespace eCommerceServer.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly DataContext _context;
        private readonly IAuthService _authService;

        //private readonly IHttpContextAccessor _httpContextAccessor;

        public CartService(DataContext context, 
            /*IHttpContextAccessor httpContextAccessor*/
            IAuthService authService)
        {
            _context = context;
            _authService = authService;
            //_httpContextAccessor = httpContextAccessor;
        }

        //was move to the AuthService
        //private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<List<CartProductResponse>>> GetCartProductsAsync(List<CartItem> cartItems)
        {
            var result = new ServiceResponse<List<CartProductResponse>>
            {
                Data = new List<CartProductResponse>()
            };

            foreach (var item in cartItems)
            {
                var product = _context.Products
                    .Include(p => p.Images)
                    .Where(p => p.Id == item.ProductId).FirstOrDefault();
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
                    Images = product.Images

                };

                result.Data.Add(cartProductResponse);
            }

            return result;
        }

        public async Task<ServiceResponse<List<CartProductResponse>>> StoreCartItemsAsync(List<CartItem> cartItems/*, int userId*/)
        {

            cartItems.ForEach(ci => ci.UserId = _authService.GetUserId() /*userId*/);
            await _context.CarItems.AddRangeAsync(cartItems);
            await _context.SaveChangesAsync();

            //return await GetCartProductsAsync(await _context.CarItems.Where(ci => ci.UserId == GetUserId() /*userId*/).ToListAsync());
            return await GetDBCartProductsAsync();
        }

        public async Task<ServiceResponse<int>> GetCartItemsCountAsync()
        {
            int count = (await _context.CarItems.Where(ci => ci.UserId == _authService.GetUserId()).ToListAsync()).Count();
            return new ServiceResponse<int>
            {
                Success = true,
                Message = string.Empty,
                Data = count
            };

        }

        public async Task<ServiceResponse<List<CartProductResponse>>> GetDBCartProductsAsync(int? userId = null)
        {
            userId = userId == null ? _authService.GetUserId() : userId;
            return await GetCartProductsAsync(await _context.CarItems.Where(ci => ci.UserId == userId).ToListAsync());
        }

        public async Task<ServiceResponse<bool>> AddToCartAsync(CartItem cartItem)
        {
            cartItem.UserId = _authService.GetUserId();
            var sameItem = await _context.CarItems.FirstOrDefaultAsync(ci => ci.UserId == cartItem.UserId && ci.ProductId == cartItem.ProductId && ci.ProductTypeId == cartItem.ProductTypeId);
            if (sameItem != null)
            {
                sameItem.Quantity += cartItem.Quantity;
            }
            else
            {
                await _context.CarItems.AddAsync(cartItem);
            }
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Success = true, Message = "Item added to cart", Data = true };
        }

        public async Task<ServiceResponse<bool>> UpdateQuantityAsync(CartItem cartItem)
        {
            cartItem.UserId = _authService.GetUserId();
            var dbCartItem = await _context.CarItems.FirstOrDefaultAsync(ci => ci.UserId == cartItem.UserId && ci.ProductId == cartItem.ProductId && ci.ProductTypeId == cartItem.ProductTypeId);
            if (dbCartItem != null)
            {
                dbCartItem.Quantity = cartItem.Quantity;
                await _context.SaveChangesAsync();
                return new ServiceResponse<bool> { Success = true, Message = "Quantity Updated", Data = true };
            }
            else
            {
                return new ServiceResponse<bool> { Success = true, Message = "Cart Item not found", Data = false };
            }

        }

        public async Task<ServiceResponse<bool>> RemoveItemFromCartAsync(int productId, int productTypeId)
        {
            var dbCartItem = await _context.CarItems.FirstOrDefaultAsync(ci => ci.UserId == _authService.GetUserId() && ci.ProductId == productId && ci.ProductTypeId == productTypeId);
            if (dbCartItem != null)
            {
                _context.CarItems.Remove(dbCartItem);
                await _context.SaveChangesAsync();
                return new ServiceResponse<bool> { Success = true, Message = "Item removed", Data = true };
            }
            else
            {
                return new ServiceResponse<bool> { Success = true, Message = "Cart Item not found", Data = false };
            }
        }
    }
}
