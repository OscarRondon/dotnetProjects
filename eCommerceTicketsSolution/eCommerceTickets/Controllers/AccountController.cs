using eCommerceTickets.Data;
using eCommerceTickets.Models;
using eCommerceTickets.Models.Enums;
using eCommerceTickets.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace eCommerceTickets.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
            AppDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        #region Login
        //GET
        public IActionResult Login()
        {
            return View(new LoginVM());
        }

        //POST
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);

            if (user != null)
            {
                var passWordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passWordCheck)
                {
                    var sigin = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (sigin.Succeeded)
                        return RedirectToAction("Index", "Movies");
                    else
                    {
                        TempData["Error"] = "Error Signing In";
                        return View(loginVM);
                    }
                }
                TempData["Error"] = "Invalid Password";
                return View(loginVM);
            }

            TempData["Error"] = "Invalid User";
            return View(loginVM);
        }
        #endregion

        #region Register
        //GET
        public IActionResult Register()
        {
            return View(new RegisterVM());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerVM);
            }

            var newUser = new ApplicationUser()
            {
                FullName = registerVM.FullName,
                Email = registerVM.EmailAddress,
                UserName = registerVM.EmailAddress
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User.ToString());

            return View("RegisterCompleted");
        }
        #endregion

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Movies");
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }
    }
}
