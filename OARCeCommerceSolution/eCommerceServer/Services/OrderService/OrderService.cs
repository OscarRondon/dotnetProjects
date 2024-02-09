
using System.Security.Claims;

namespace eCommerceServer.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _context;
        private readonly ICartService _cartService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public OrderService(
            DataContext context,
            ICartService cartService,
            IHttpContextAccessor httpContextAccessor) 
        {
            _context = context;
            _cartService = cartService;
            _httpContextAccessor = httpContextAccessor;
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
                UserId = GetUserId(),
                OrderItems = orderItems,
                TotalPrice = totalPrice
            };

            _context.Add(order);
            await _context.SaveChangesAsync();

            response.Success = true;
            response.Message = "Order placed successfully";
            response.Data = true;

            return response;
        }
    }
}
