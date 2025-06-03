using Shortly.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Data.Services
{
    public interface IUrlsService
    {
        List<Url> GetUrls();
        Url GetById(int id);
        Url Create(Url url);
        void Delete(int id);
        Url Update(int id, Url url);

    }
}
