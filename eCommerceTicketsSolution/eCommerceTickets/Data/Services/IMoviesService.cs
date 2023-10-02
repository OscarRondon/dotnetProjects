using eCommerceTickets.Data.Base;
using eCommerceTickets.Models;

namespace eCommerceTickets.Data.Services
{
    public interface IMoviesService: IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieById(int id);
    }
}
