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

        public List<User> GetUsers()
        {
            var allUsers = _context.Users.Include(l => l.Urls).ToList();
            return allUsers;
        }
    }
}
