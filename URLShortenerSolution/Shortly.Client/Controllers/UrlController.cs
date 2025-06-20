using AutoMapper;
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
        private readonly IMapper _mapper;

        //private readonly AppDBContext _dbContext;

        public UrlController(/*AppDBContext dbContext*/IUrlsService urlsService, IMapper mapper)
        {
            _urlsService = urlsService;
            _mapper = mapper;
            //_dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            //hadcoded data-------------------------------
            /*
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
            */
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
            var allUrls = await _urlsService.GetUrlsAsync();
            var mappedAllUrls = _mapper.Map<List<GetUrlVM>>(allUrls);
            /*
                .Select(url => new GetUrlVM()
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
            */
            return View(mappedAllUrls);
        }

        public async Task<IActionResult> Create()
        {
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Remove(int linkIdToRemove)
        {
            var urlToRemove = await _urlsService.GetByIdAsync(linkIdToRemove);
            if (urlToRemove == null)
            {
                return NotFound();
            }
            await _urlsService.DeleteAsync(linkIdToRemove);

            /*
            var urlToRemove = _dbContext.Urls.FirstOrDefault(url => url.Id == linkIdToRemove);
            _dbContext.Urls.Remove(urlToRemove);
            _dbContext.SaveChanges();
            */
            //return View();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveAsRouteData(int id)
        {
            var urlToRemove = await _urlsService.GetByIdAsync(id);
            if (urlToRemove == null)
            {
                return NotFound();
            }
            await _urlsService.DeleteAsync(id);
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
