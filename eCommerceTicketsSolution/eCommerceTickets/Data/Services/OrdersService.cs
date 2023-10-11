using eCommerceTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerceTickets.Data.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly AppDbContext _context;

        public OrdersService(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrdersByUserId(string userId)
        {
            var orders = await _context.Orders.Include(oi => oi.OrderItems).ThenInclude(m => m.Movie).Where(o => o.UserId == userId).ToListAsync();
            return orders;
        }

        public async Task StoreOrder(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Quantity = item.Quantity,
                    MovieId = item.Movie.Id,
                    OrderId = order.Id,
                    Ammount = item.Movie.Price
                };
                await _context.OrderItems.AddAsync(orderItem);
            }
            await _context.SaveChangesAsync();
        }
    }
}
