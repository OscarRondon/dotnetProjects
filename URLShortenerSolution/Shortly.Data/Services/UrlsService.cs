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

        public async Task<Url> CreateAsync(Url url)
        {
            await _context.Urls.AddAsync(url);
            await _context.SaveChangesAsync();
            return url;
        }

        public async Task DeleteAsync(int id)
        {
            var urlDB = _context.Urls.FirstOrDefault(u => u.Id == id);
            if (urlDB != null)
            {
                _context.Urls.Remove(urlDB);
                await _context.SaveChangesAsync();
            }
            
        }

        public async Task<Url> GetByIdAsync(int id)
        {
            return await _context.Urls.Include(u => u.User).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<Url>> GetUrlsAsync()
        {
            var allUrls = await _context.Urls.Include(u => u.User).ToListAsync();
            return allUrls;
        }

        public async Task<Url> UpdateAsync(int id, Url url)
        {
            var urlDB = await _context.Urls.FirstOrDefaultAsync(u => u.Id == id);
            if (urlDB != null)
            {
                urlDB.OriginalLink = url.OriginalLink;
                urlDB.ShortLink = url.ShortLink;
                urlDB.NrOfClicks = url.NrOfClicks;
                urlDB.UserId = url.UserId;
                urlDB.DateUpdated = DateTime.Now;
                //_context.Urls.Update(urlDB); //this line is optional as we are modifying the existing entity
                await _context.SaveChangesAsync();
            }
            return urlDB;

        }
    }
}
