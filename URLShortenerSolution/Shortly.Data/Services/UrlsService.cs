using Microsoft.EntityFrameworkCore;
using Shortly.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Data.Services
{
    public class UrlsService: IUrlsService
    {
        private AppDBContext _context;

        public UrlsService(AppDBContext context)
        {
            _context = context;
        }

        public Url Create(Url url)
        {
            _context.Urls.Add(url);
            _context.SaveChanges();
            return url;
        }

        public void Delete(int id)
        {
            var urlDB = _context.Urls.FirstOrDefault(u => u.Id == id);
            if (urlDB != null)
            {
                _context.Urls.Remove(urlDB);
                _context.SaveChanges();
            }
            
        }

        public Url GetById(int id)
        {
            return _context.Urls.Include(u => u.User).FirstOrDefault(u => u.Id == id);
        }

        public List<Url> GetUrls()
        {
            var allUrls = _context.Urls.Include(u => u.User).ToList();
            return allUrls;
        }

        public Url Update(int id, Url url)
        {
            var urlDB = _context.Urls.FirstOrDefault(u => u.Id == id);
            if (urlDB != null)
            {
                urlDB.OriginalLink = url.OriginalLink;
                urlDB.ShortLink = url.ShortLink;
                urlDB.NrOfClicks = url.NrOfClicks;
                urlDB.UserId = url.UserId;
                urlDB.DateUpdated = DateTime.Now;
                _context.Urls.Update(urlDB); //this line is optional as we are modifying the existing entity
                _context.SaveChanges();
            }
            return urlDB;

        }
    }
}
