using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shortly.Client.Data.ViewMoels;
using Shortly.Data;
using Shortly.Data.Models;
using Shortly.Data.Services;

namespace Shortly.Client.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUsersService _usersService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        //private readonly AppDBContext _context;

        public AuthenticationController(/*AppDBContext context*/ 
            IUsersService usersService,
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager
            )
        {
            //_context = context;
            _usersService = usersService;
            _signInManager = signInManager;
            _userManager = userManager;
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

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if(user != null)
            {
                var userPasswordCheck = await _userManager.CheckPasswordAsync(user, loginVM.EmailPassword);
                if(userPasswordCheck)
                {
                    // User exists and password is correct
                    // Sign in the user
                    var userLoggedIn = await _signInManager.PasswordSignInAsync(user, loginVM.EmailPassword, false, false);
                    if (userLoggedIn.Succeeded)
                    {
                        // User successfully logged in
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {                         
                        ModelState.AddModelError("", "Check your user and password.");
                        return View("Login", loginVM);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Check your user and password.");
                    return View("Login", loginVM);
                }
            }
            else
            {
                ModelState.AddModelError("", "User not found.");
                return View("Login", loginVM);
            }
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
