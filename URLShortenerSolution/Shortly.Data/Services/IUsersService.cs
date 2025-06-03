using Shortly.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Data.Services
{
    public interface IUsersService
    {
        List<User> GetUsers();
        Url GetById(int id);
        User Create(User url);
        void Delete(int id);
        Url Update(int id, User url);
    }
}
