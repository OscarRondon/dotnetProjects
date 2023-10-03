using eCommerceTickets.Data.Base;
using eCommerceTickets.Models;
using eCommerceTickets.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace eCommerceTickets.Data.Services
{
    public class MoviesService: EntityBaseRepository<Movie>, IMoviesService
    {
        private readonly AppDbContext _context;
        public MoviesService(AppDbContext context): base(context) 
        {
            _context = context;
        }

        public async Task<Movie> GetMovieById(int id)
        {
            return await _context.Movies
                .Include(p => p.Producer)
                .Include(c => c.Cinema)
                .Include(am => am.ActorsMovies).ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues()
        {
            var response = new NewMovieDropdownsVM()
            {
                Actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync(),
                Producers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync()
            };

            return response;
        }
    }
}
