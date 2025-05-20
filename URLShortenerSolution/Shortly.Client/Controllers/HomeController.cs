using Microsoft.AspNetCore.Mvc;
using Shortly.Client.Data.ViewMoels;
using Shortly.Client.Models;
using Shortly.Data;
using Shortly.Data.Models;
using System.Diagnostics;

namespace Shortly.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDBContext _context;

        public HomeController(ILogger<HomeController> logger, AppDBContext appDBContext)
        {
            _logger = logger;
            _context = appDBContext;
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

            var newURL = new Url()
            {
                OriginalLink = postUrlVM.Url,
                ShortLink = GenerateShortUrl(6),
                NrOfClicks = 0,
                UserId = null,
                DateCreated = DateTime.UtcNow,
                DateUpdated = null
            };

            _context.Urls.Add(newURL);
            _context.SaveChanges();

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

        private string GenerateShortUrl(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
