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

        public async Task AddNewMovie(NewMovieVM movie)
        {
            var newMovie = new Movie()
            {
                Name = movie.Name,
                Description = movie.Description,
                Price = movie.Price,
                Image = movie.Image,
                CinemaId = movie.CinemaId,
                StartDate = movie.StartDate,
                EndDate = movie.EndDate,
                MovieCategory = movie.MovieCategory,
                ProducerId = movie.ProducerId
            };
            await _context.Movies.AddAsync(newMovie);
            await _context.SaveChangesAsync();

            //Add Movie Actors
            foreach (var actorId in movie.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = newMovie.Id,
                    ActorId = actorId
                };
                await _context.ActorsMovies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Movie> GetMovieById(int id)
        {
            return await _context.Movies
                .Include(p => p.Producer)
                .Include(c => c.Cinema)
                .Include(am => am.ActorsMovies).ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateMovie(NewMovieVM movie)
        {
            var dbMovie = await _context.Movies.FirstOrDefaultAsync(n => n.Id == movie.Id);

            if (dbMovie != null)
            {
                dbMovie.Name = movie.Name;
                dbMovie.Description = movie.Description;
                dbMovie.Price = movie.Price;
                dbMovie.Image = movie.Image;
                dbMovie.CinemaId = movie.CinemaId;
                dbMovie.StartDate = movie.StartDate;
                dbMovie.EndDate = movie.EndDate;
                dbMovie.MovieCategory = movie.MovieCategory;
                dbMovie.ProducerId = movie.ProducerId;
                await _context.SaveChangesAsync();
            }

            //Remove existing actors
            var existingActorsDb = _context.ActorsMovies.Where(n => n.MovieId == movie.Id).ToList();
            _context.ActorsMovies.RemoveRange(existingActorsDb);
            await _context.SaveChangesAsync();

            //Add Movie Actors
            foreach (var actorId in movie.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = movie.Id,
                    ActorId = actorId
                };
                await _context.ActorsMovies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
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
