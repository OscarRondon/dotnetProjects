using Microsoft.AspNetCore.Mvc;
using Shortly.Client.Data.ViewMoels;

namespace Shortly.Client.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Users()
        {
            return View();
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
            return View();
        }
    }
}
