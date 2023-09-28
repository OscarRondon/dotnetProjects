using Microsoft.AspNetCore.Mvc;

namespace eCommerceTickets.Controllers
{
    public class ErrorsController : Controller
    {
        
        public IActionResult NotFound()
        {
            string originalPath = "unknown";
            if (HttpContext.Items.ContainsKey("originalPath"))
            {
                originalPath = HttpContext.Items["originalPath"] as string;
            }
            return View();
        }
    }
}
