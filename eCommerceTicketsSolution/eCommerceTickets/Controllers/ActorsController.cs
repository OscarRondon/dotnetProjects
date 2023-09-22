using eCommerceTickets.Data;
using eCommerceTickets.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceTickets.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorsService _service;

        public ActorsController(IActorsService service) 
        {
            _service = service;
        }

        //Get: Actors/Index
        public async Task<IActionResult> Index()
        {
            var actors = await _service.GetAll();
            return View(actors);
        }

        //Get: Actors/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }
    }
}
