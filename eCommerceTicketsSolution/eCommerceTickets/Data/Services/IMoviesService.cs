using eCommerceTickets.Data.Base;
using eCommerceTickets.Models;
using eCommerceTickets.ViewModels;

namespace eCommerceTickets.Data.Services
{
    public interface IMoviesService: IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieById(int id);

        Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues();

        Task AddNewMovie(NewMovieVM movie);

        Task UpdateMovie(NewMovieVM movie);
    }
}
