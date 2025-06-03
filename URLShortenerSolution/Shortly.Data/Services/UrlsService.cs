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
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Url GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Url> GetUrls()
        {
            var allUrls = _context.Urls.Include(u => u.User).ToList();
            return allUrls;
        }

        public Url Update(int id, Url url)
        {
            throw new NotImplementedException();
        }
    }
}
