using Microsoft.EntityFrameworkCore;
using Shortly.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Data.Services
{
    public class UsersService: IUsersService
    {
        private AppDBContext _context;

        public UsersService(AppDBContext context)
        {
            _context = context;
        }

        public User Create(User url)
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

        public List<User> GetUsers()
        {
            var allUsers = _context.Users.Include(l => l.Urls).ToList();
            return allUsers;
        }

        public Url Update(int id, User url)
        {
            throw new NotImplementedException();
        }
    }
}
