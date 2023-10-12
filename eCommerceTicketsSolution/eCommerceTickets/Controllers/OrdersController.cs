using eCommerceTickets.Data.Services;
using eCommerceTickets.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceTickets.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IMoviesService _moviesService;
        private readonly IOrdersService _ordersService;
        private readonly ShoppingCartService _shopingCartService;

        public OrdersController(IMoviesService moviesService, ShoppingCartService shopingCartService, IOrdersService ordersService)
        {
            _moviesService = moviesService;
            _ordersService = ordersService;
            _shopingCartService = shopingCartService;
        }

        //GET: Index
        public async Task<IActionResult> Index()
        {
            string userId = "";
            var orders = await _ordersService.GetOrdersByUserId(userId);
            return View(orders);
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

        public async Task<IActionResult> CompleteOrder()
        {
            string userId = "";
            string userEmailAddress = "";
            var items = await _shopingCartService.GetShoppingCartItems();

            await _ordersService.StoreOrder(items, userId, userEmailAddress);
            await _shopingCartService.ClearShoppingCart();

            return View("OrderCompleted");
        }
    }
}
