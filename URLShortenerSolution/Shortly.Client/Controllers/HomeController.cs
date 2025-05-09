﻿using Microsoft.AspNetCore.Mvc;
using Shortly.Client.Data.ViewMoels;
using Shortly.Client.Models;
using System.Diagnostics;

namespace Shortly.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var postUlrVM = new PostUrlVM();
            return View(postUlrVM);
        }

        public IActionResult ShrotenUrl(PostUrlVM postUrlVM)
        {
            //Validate Model
            if (!ModelState.IsValid)
            {
                return View("Index", postUrlVM);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
