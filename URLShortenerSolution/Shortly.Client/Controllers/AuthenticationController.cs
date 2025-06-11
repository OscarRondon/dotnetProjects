using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Users()
        {
            /*
            var users = _context
                .Users
                .Include(u => u.Urls)
                .ToList();
            */
            var users = _usersService.GetUsers();
            return View(users);
        }

        public IActionResult Login()
        {
            var initial = new LoginVM();
            return View(initial);
        }

        public IActionResult LoginSubmitted(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View("Login",loginVM);
            }
            return RedirectToAction("Index", "Home");
            // return Ok(new { Message = "Data processed successfully!" });
        }

        public IActionResult Register()
        {
            return View(new RegisterVM());
        }

        public IActionResult RegisterUser(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", registerVM);
            }
            return RedirectToAction("Index","Home");
        }
    }
}
