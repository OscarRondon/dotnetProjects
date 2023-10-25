using eCommerceTickets.Data;
using eCommerceTickets.Data.Services;
using eCommerceTickets.Models;
using eCommerceTickets.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceTickets.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProducersController : Controller
    {
        private readonly IProducersService _service;

        public ProducersController(IProducersService service) 
        {
            _service = service;
        }

        //Get: Producers Index
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var producers = await _service.GetAll();
            return View(producers);
        }

        //Get: Producers/Details/Id
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var producer = await _service.GetById(id);
            if (producer == null)
                return Redirect("/Errors/NotFound");

            return View(producer);
        }

        //Get: Producers/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        //Get: Poducer/Edit/Id
        public async Task<IActionResult> Edit(int id)
        {
            var producer = await _service.GetById(id);
            if (producer == null)
                return Redirect("/Errors/NotFound");

            return View(producer);
        }

        //Get: Producer/Delete/Id
        public async Task<IActionResult> Delete(int id)
        {
            var producer = await _service.GetById(id);
            if (producer == null)
                return Redirect("/Errors/NotFound");

            return View(producer);
        }


        //Post: Producers/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName, ProfilePicture, Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
            await _service.Add(producer);
            return RedirectToAction(nameof(Index));
        }

        //Post: Producers/Edit/Id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, FullName, ProfilePicture, Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
            await _service.Update(id, producer);
            return RedirectToAction(nameof(Index));
        }

        //Post: Producers/Delete/Id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producer = await _service.GetById(id);
            if (producer == null)
                return Redirect("/Errors/NotFound");
            await _service.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
