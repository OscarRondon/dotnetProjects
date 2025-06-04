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

        public User Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public void Delete(int id)
        {
            var userDB = _context.Users.FirstOrDefault(u => u.Id == id);
            if (userDB == null)
            {
                throw new Exception("User not found");
            }
            _context.Users.Remove(userDB);
            _context.SaveChanges();
        }

        public User? GetById(int id)
        {
            return _context.Users.Include(l => l.Urls).FirstOrDefault(u => u.Id == id);
        }

        public List<User> GetUsers()
        {
            var allUsers = _context.Users.Include(l => l.Urls).ToList();
            return allUsers;
        }

        public User Update(int id, User user)
        {
            var userDB = _context.Users.FirstOrDefault(u => u.Id == id);
            if (userDB == null)
            {
                throw new Exception("User not found");
            }
            userDB.FullName = user.FullName;
            userDB.Email = user.Email;
            userDB.Urls = user.Urls;
            _context.Users.Update(userDB); //this line is optional as we are modifying the existing entity
            _context.SaveChanges();
            return userDB;
        }
    }
}
