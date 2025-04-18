using Microsoft.AspNetCore.Mvc;
using Shortly.Client.Data.Models;
using Shortly.Client.Data.ViewMoels;

namespace Shortly.Client.Controllers
{
    public class UrlController : Controller
    {
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
            return View(allUrls);
        }

        public IActionResult Create()
        {
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int linkIdToRemove)
        {
            return View();
            //return RedirectToAction("Index");
        }

        public IActionResult RemoveAsRouteData(int id)
        {
            return View();
            //return RedirectToAction("Index");
        }
    }
}
