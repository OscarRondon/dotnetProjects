﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shortly.Client.Data.ViewMoels;
using Shortly.Data;
using Shortly.Data.Services;

namespace Shortly.Client.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUsersService _usersService;

        //private readonly AppDBContext _context;

        public AuthenticationController(/*AppDBContext context*/ IUsersService usersService)
        {
            //_context = context;
            _usersService = usersService;
        }

        public async Task<IActionResult> Users()
        {
            /*
            var users = _context
                .Users
                .Include(u => u.Urls)
                .ToList();
            */
            var users = await _usersService.GetUsersAsync();
            return View(users);
        }

        public async Task<IActionResult> Login()
        {
            var initial = new LoginVM();
            return View(initial);
        }

        public async Task<IActionResult> LoginSubmitted(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View("Login",loginVM);
            }
            return RedirectToAction("Index", "Home");
            // return Ok(new { Message = "Data processed successfully!" });
        }

        public async Task<IActionResult> Register()
        {
            return View(new RegisterVM());
        }

        public async Task<IActionResult> RegisterUser(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", registerVM);
            }
            return RedirectToAction("Index","Home");
        }
    }
}
