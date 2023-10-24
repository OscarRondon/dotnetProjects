using eCommerceTickets.Data.Base;
using eCommerceTickets.Models;

namespace eCommerceTickets.Data.Services
{
    public interface IOrdersService
    {
        Task StoreOrder(List<ShoppingCartItem> items, string userId, string userEmailAddress);

        Task<List<Order>> GetOrdersByUserId(string userId);

        Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole);
    }
}
