using eCommerceTickets.Data.Services;
using eCommerceTickets.Models;

namespace eCommerceTickets.ViewModels
{
    public class ShoppingCartVM
    {
        public ShoppingCartService shoppingCart { get; set; }
        public double shoppingCartTotal;
    }
}
