
using System.Security.Claims;

namespace eCommerceServer.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _context;
        private readonly ICartService _cartService;
        private readonly IAuthService _authService;

        //private readonly IHttpContextAccessor _httpContextAccessor;

        //was move to the AuthService
        //private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public OrderService(
            DataContext context,
            ICartService cartService,
            /*IHttpContextAccessor httpContextAccessor*/
            IAuthService authService) 
        {
            _context = context;
            _cartService = cartService;
            _authService = authService;
            //_httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<OrderDetailsResponse>> GetOrderDetailsAsync(int orderId)
        {
            var response = new ServiceResponse<OrderDetailsResponse>();
            var orders = await _context.Orders
                .Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
                .Include(o => o.OrderItems).ThenInclude(oi => oi.ProductType)
                .Where(o => o.UserId == _authService.GetUserId() && o.Id == orderId)
                .OrderByDescending(o => o.OrderDate)
                .FirstOrDefaultAsync();
            if(orders == null)
            {
                response.Success = false;
                response.Message = "Order not found.";
                return response;
            }

            var orderDetailsResponse = new OrderDetailsResponse
            {
                OrderDate = orders.OrderDate,
                TotalPrice = orders.TotalPrice,
                Products = new List<OrderDetailsProductResponse>()
            };

            orders.OrderItems.ForEach(oi => orderDetailsResponse.Products.Add(new OrderDetailsProductResponse
            {
                ProductId = oi.ProductId,
                Title = oi.Product.Title,
                ImageUrl = oi.Product.ImageUrl,
                ProductType = oi.ProductType.Name,
                Quantity = oi.Quantity,
                TotalPrice = oi.TotalPrice
            }));

            response.Success = true;
            response.Message = string.Empty;
            response.Data = orderDetailsResponse;
            return response;

        }

        public async Task<ServiceResponse<List<OrderOverviewResponse>>> GetOrdersAsync()
        {
            var response = new ServiceResponse<List<OrderOverviewResponse>>
            {
                Success = true,
                Message = string.Empty
            };

            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.UserId == _authService.GetUserId())
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            var orderResponse = new List<OrderOverviewResponse>();
            orders.ForEach(o => orderResponse.Add(new OrderOverviewResponse
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                Product = o.OrderItems.Count > 1 ?
                $"{o.OrderItems.First().Product.Title} and {o.OrderItems.Count -1} more Items..":
                $"{o.OrderItems.First().Product.Title}",
                ProductImageUrl = o.OrderItems.First().Product.ImageUrl,
                TotalPrice = o.TotalPrice,

            }));

            response.Data = orderResponse;
            return response;
        }

        public async Task<ServiceResponse<bool>> PlaceOrderAsync()
        {
            var response = new ServiceResponse<bool>
            {
                Success = false,
                Message = "Error placing the order",
                Data = false
            };
            var products = (await _cartService.GetDBCartProductsAsync()).Data;
            if(products == null) 
            {
                response.Message = "No orders valiable to place";
                return response;
            }

            decimal totalPrice = 0;
            totalPrice = products.Sum(x => x.Price*x.Quantity);

            var orderItems = new List<OrderItem>();
            foreach (var product in products)
            {
                orderItems.Add(new OrderItem
                {
                    ProductId = product.ProductId,
                    ProductTypeId = product.ProductTypeId,
                    Quantity = product.Quantity,
                    TotalPrice = product.Price * product.Quantity
                });
            }

            var order = new Order
            {
                UserId = _authService.GetUserId(),
                OrderItems = orderItems,
                TotalPrice = totalPrice
            };

            _context.Add(order);
            _context.RemoveRange(_context.CarItems.Where(ci => ci.UserId == _authService.GetUserId()));
            await _context.SaveChangesAsync();

            response.Success = true;
            response.Message = "Order placed successfully";
            response.Data = true;

            return response;
        }
    }
}
