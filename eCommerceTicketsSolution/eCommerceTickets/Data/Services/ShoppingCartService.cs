using eCommerceTickets.Data.Base;
using eCommerceTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerceTickets.Data.Services
{
    public class ShoppingCartService
    {
        private readonly AppDbContext _context;

        public string ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCartService(AppDbContext context, IServiceProvider service)
        {
            _context = context;

            //ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            //string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            //session.SetString("cartId", cartId);
            //ShoppingCartId = cartId;

        }

        public static ShoppingCartService GetShoppingCart(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = service.GetService<AppDbContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);
            return new ShoppingCartService(context, service) { ShoppingCartId = cartId } ;
        }

        public async Task AddShoppingCartItem(Movie movie)
        {
            var cartItem = await _context.ShoppingCartItems.FirstOrDefaultAsync(sc => sc.ShoppingCartId == ShoppingCartId && sc.Movie.Id == movie.Id);

            if (cartItem == null)
            {
                cartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Movie = movie,
                    Quantity = 1
                };
                _context.ShoppingCartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }
            await _context.SaveChangesAsync();
        }

        public async Task RemoveShoppingCartItem(Movie movie)
        {
            var cartItem = await _context.ShoppingCartItems.FirstOrDefaultAsync(sc => sc.ShoppingCartId == ShoppingCartId && sc.Movie.Id == movie.Id);

            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                    cartItem.Quantity--;
                else
                {
                    _context.ShoppingCartItems.Remove(cartItem);
                };
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ShoppingCartItem>> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = await _context.ShoppingCartItems.Where(sc => sc.ShoppingCartId == ShoppingCartId).Include(m => m.Movie).ToListAsync());
        }

        public async Task<double> GetShoppingCartTotal()
        {
            var total = await _context.ShoppingCartItems.Where(sc => sc.ShoppingCartId == ShoppingCartId).Select(m => m.Movie.Price * m.Quantity).SumAsync();
            return total;
        }

        public async Task ClearShoppingCart()
        {
            var carItems = await _context.ShoppingCartItems.Where(sc => sc.ShoppingCartId == ShoppingCartId).ToListAsync();
            _context.ShoppingCartItems.RemoveRange(carItems);
            await _context.SaveChangesAsync();
        }
    }
}
