using eCommerceTickets.Data;
using eCommerceTickets.Data.Services;
using eCommerceTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceTickets.Controllers
{
    public class CinemasController : Controller
    {
        private readonly ICinemasService _service;
        public CinemasController(ICinemasService service) 
        {
            _service = service;
        }

        //Get: Cinemas Index
        public async Task<IActionResult> Index()
        {
            var cinemas = await _service.GetAll();
            return View(cinemas);
        }

        //Get: Cinemas/Details/Id
        public async Task<IActionResult> Details(int id)
        {
            var cinema = await _service.GetById(id);
            if (cinema == null)
                return Redirect("/Errors/NotFound");

            return View(cinema);
        }

        //Get: Cinemas/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        //Get: Poducer/Edit/Id
        public async Task<IActionResult> Edit(int id)
        {
            var cinema = await _service.GetById(id);
            if (cinema == null)
                return Redirect("/Errors/NotFound");

            return View(cinema);
        }

        //Get: Producer/Delete/Id
        public async Task<IActionResult> Delete(int id)
        {
            var cinema = await _service.GetById(id);
            if (cinema == null)
                return Redirect("/Errors/NotFound");

            return View(cinema);
        }


        //Post: Cinemas/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name, Logo, Description")] Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }
            await _service.Add(cinema);
            return RedirectToAction(nameof(Index));
        }

        //Post: Cinemas/Edit/Id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Logo, Description")] Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }
            await _service.Update(id, cinema);
            return RedirectToAction(nameof(Index));
        }

        //Post: Cinemas/Delete/Id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cinema = await _service.GetById(id);
            if (cinema == null)
                return Redirect("/Errors/NotFound");
            await _service.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
