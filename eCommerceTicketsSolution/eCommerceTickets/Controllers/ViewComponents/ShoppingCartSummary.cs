using eCommerceTickets.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceTickets.Controllers.ViewComponents
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly ShoppingCartService _shoppingCart;

        public ShoppingCartSummary(ShoppingCartService shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await _shoppingCart.GetShoppingCartItems();
            return View(items.Count);
        }
    }
}
