﻿using eCommerceTickets.Data;
using eCommerceTickets.Data.Services;
using eCommerceTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceTickets.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ActorsController : Controller
    {
        private readonly IActorsService _service;

        public ActorsController(IActorsService service) 
        {
            _service = service;
        }

        //Get: Actors/Index
        [AllowAnonymous]
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
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var actor = await _service.GetById(id);
            if (actor == null)
                return Redirect("/Errors/NotFound");

            return View(actor);
        }

        //Get: Actors/Edit/Id
        public async Task<IActionResult> Edit(int id)
        {
            var actor = await _service.GetById(id);
            if (actor == null)
                return Redirect("/Errors/NotFound");

            return View(actor);
        }

        //Get: Actors/Delete/Id
        public async Task<IActionResult> Delete(int id)
        {
            var actor = await _service.GetById(id);
            if (actor == null)
                return Redirect("/Errors/NotFound");

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
            await _service.Add(actor);
            return RedirectToAction(nameof(Index));
        }

        //Post: Actors/Edit/Id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, FullName, ProfilePicture, Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _service.Update(id, actor);
            return RedirectToAction(nameof(Index));
        }

        //Post: Actors/Delete/Id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actor = await _service.GetById(id);
            if (actor == null)
                return Redirect("/Errors/NotFound");
            await _service.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
