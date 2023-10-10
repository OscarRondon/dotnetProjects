using eCommerceTickets.Data.Services;
using eCommerceTickets.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceTickets.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IMoviesService _moviesService;
        private readonly ShoppingCartService _shopingCartService;


        public OrdersController(IMoviesService moviesService, ShoppingCartService shopingCartService)
        {
            _moviesService = moviesService;
            _shopingCartService = shopingCartService;
        }

        //GET: ShopingCart
        public async Task<IActionResult> ShoppingCart()
        {
            var shoppingCartItems = await _shopingCartService.GetShoppingCartItems();
            _shopingCartService.ShoppingCartItems = shoppingCartItems;
            var response = new ShoppingCartVM()
            {
                shoppingCart = _shopingCartService,
                shoppingCartTotal = await _shopingCartService.GetShoppingCartTotal()
            };
            return View(response);
        }

        public async Task<RedirectToActionResult> AddToShoppingCart(int id)
        {
            var movie = await _moviesService.GetMovieById(id);

            if (movie != null)
            {
                await _shopingCartService.AddShoppingCartItem(movie);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<RedirectToActionResult> RemoveItemFromShoppingCart(int id)
        {
            var movie = await _moviesService.GetMovieById(id);

            if (movie != null)
            {
                await _shopingCartService.RemoveShoppingCartItem(movie);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
    }
}
