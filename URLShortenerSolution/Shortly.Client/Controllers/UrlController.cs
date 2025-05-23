using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shortly.Client.Data.ViewMoels;
using Shortly.Data;

namespace Shortly.Client.Controllers
{
    public class UrlController : Controller
    {
        private readonly AppDBContext _dbContext;

        public UrlController(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            //hadcoded data-------------------------------
            var allUrls = new List<GetUrlVM>()
            {
                new GetUrlVM()
                {
                    Id = 1,
                    OriginalLink = "http://link1.com",
                    ShortLink = "sh1",
                    NrOfClicks = 1,
                    UserId = 1,
                },
                new GetUrlVM()
                {
                    Id = 2,
                    OriginalLink = "http://link2.com",
                    ShortLink = "sh2",
                    NrOfClicks = 5,
                    UserId = 2,
                },
                new GetUrlVM()
                {
                    Id = 3,
                    OriginalLink = "http://link3.com",
                    ShortLink = "sh3",
                    NrOfClicks = 3,
                    UserId = 3,
                }
            };
            //---------------------------------------------

            allUrls = _dbContext.Urls.Select(url => new GetUrlVM() 
            {
                Id = url.Id,
                OriginalLink = url.OriginalLink,
                ShortLink = url.ShortLink,
                NrOfClicks = url.NrOfClicks,
                UserId = url.UserId,
            }).ToList();
            return View(allUrls);
        }

        public IActionResult Create()
        {
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int linkIdToRemove)
        {
            var urlToRemove = _dbContext.Urls.FirstOrDefault(url => url.Id == linkIdToRemove);
            _dbContext.Urls.Remove(urlToRemove);
            _dbContext.SaveChanges();
            //return View();
            return RedirectToAction("Index");
        }

        public IActionResult RemoveAsRouteData(int id)
        {
            var urlToRemove = _dbContext.Urls.FirstOrDefault(url => url.Id == id);
            _dbContext.Urls.Remove(urlToRemove);
            _dbContext.SaveChanges();
            //return View();
            return RedirectToAction("Index");
        }
    }
}
