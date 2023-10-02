using eCommerceTickets.Data.Base;
using eCommerceTickets.Models;
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
    }
}
