using eCommerceTickets.Data;
using eCommerceTickets.Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceTickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service;

        public MoviesController(IMoviesService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _service.GetAll(c => c.Cinema, p => p.Producer);
            return View(movies);
        }

        //GET: Movies/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _service.GetMovieById(id);
            return View(movie);
        }

        //GET: Movies/Create
        public IActionResult create()
        {
            ViewData["welcome"] = "Welcome to my store";
            return View();
        }
    }
}
