using Microsoft.AspNetCore.Mvc;
using Shortly.Client.Data.Models;

namespace Shortly.Client.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            //Data from DB
            //------------- Model data passing -----------------
            var urlDB = new Url()
            {
                Id = 1,
                OriginalLink = "https://original.com",
                ShortLink = "shrtly",
                NrOfCliecks = 1,
                UserId = 1,
            };
            var allData = new List<Url>();
            allData.Add(urlDB);
            //--------------------------------------------------

            //------------- ViewData data passing --------------
            ViewData["ShortenedUrl"] = "This is just a short URL in ViewData";
            ViewData["AllUrls"] = new List<string> { "Url 1 VD", "Url 2 VD", "Url 3 VD" };
            //--------------------------------------------------

            //------------- ViewBag data passing --------------
            ViewBag.ShortenedUrlVB = "This is just a short URL in ViewBag";
            ViewBag.AllUrlsVB = new List<string> { "Url 1 VB", "Url 2 VB", "Url 3 VB" };
            //--------------------------------------------------

            //------------- TempData data passing --------------
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
            }
            //--------------------------------------------------


            return View(allData);
        }

        public IActionResult Create()
        {
            //------------- TempData data passing --------------
            var shortenedUlr = "short";
            TempData["SuccessMessage"] = "Successful";
            //--------------------------------------------------

            return RedirectToAction("Index");
        }
    }
}
