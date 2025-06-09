using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shortly.Client.Data.ViewMoels;
using Shortly.Data;
using Shortly.Data.Services;

namespace Shortly.Client.Controllers
{
    public class UrlController : Controller
    {
        private readonly IUrlsService _urlsService;

        //private readonly AppDBContext _dbContext;

        public UrlController(/*AppDBContext dbContext*/IUrlsService urlsService)
        {
            _urlsService = urlsService;
            //_dbContext = dbContext;
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
            /*
            allUrls = _dbContext
                .Urls
                .Include(u => u.User)
                .Select(url => new GetUrlVM()
                {
                    Id = url.Id,
                    OriginalLink = url.OriginalLink,
                    ShortLink = url.ShortLink,
                    NrOfClicks = url.NrOfClicks,
                    UserId = url.UserId,
                    User = url.User != null?  new GetUserVM()
                    {
                        Id = url.User.Id,
                        FullName = url.User.FullName,
                        Email = url.User.Email
                    }: null

                }).ToList();
            */
            allUrls = _urlsService.GetUrls().Select(url => new GetUrlVM()
            {
                Id = url.Id,
                OriginalLink = url.OriginalLink,
                ShortLink = url.ShortLink,
                NrOfClicks = url.NrOfClicks,
                UserId = url.UserId,
                User = url.User != null ? new GetUserVM()
                {
                    Id = url.User.Id,
                    FullName = url.User.FullName,
                    Email = url.User.Email
                } : null
            }).ToList();
            return View(allUrls);
        }

        public IActionResult Create()
        {
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int linkIdToRemove)
        {
            var urlToRemove = _urlsService.GetById(linkIdToRemove);
            if (urlToRemove == null)
            {
                return NotFound();
            }
            _urlsService.Delete(linkIdToRemove);

            /*
            var urlToRemove = _dbContext.Urls.FirstOrDefault(url => url.Id == linkIdToRemove);
            _dbContext.Urls.Remove(urlToRemove);
            _dbContext.SaveChanges();
            */
            //return View();
            return RedirectToAction("Index");
        }

        public IActionResult RemoveAsRouteData(int id)
        {
            var urlToRemove = _urlsService.GetById(id);
            if (urlToRemove == null)
            {
                return NotFound();
            }
            _urlsService.Delete(id);
            /*
            var urlToRemove = _dbContext.Urls.FirstOrDefault(url => url.Id == id);
            _dbContext.Urls.Remove(urlToRemove);
            _dbContext.SaveChanges();
            */
            //return View();
            return RedirectToAction("Index");
        }
    }
}
