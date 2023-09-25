using eCommerceTickets.Data;
using eCommerceTickets.Data.Services;
using eCommerceTickets.Models;
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

        //Get: Actors/Details/Id
        public async Task<IActionResult> Details(int id)
        {
            var actor = await _service.GetById(id);
            if (actor == null)
                return View("Empty");

            return View(actor);
        }

        //Post: Actors/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName, ProfilePicture, Bio")]Actor actor)
        {
            if(!ModelState.IsValid) 
            {
                return View(actor);
            }
            _service.Add(actor);
            return RedirectToAction(nameof(Index));
        }
    }
}
