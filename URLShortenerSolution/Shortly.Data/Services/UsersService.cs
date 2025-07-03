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

        public async Task<AppUser> CreateAsync(AppUser user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteAsync(string id)
        {
            var userDB = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (userDB != null)
            {
                _context.Users.Remove(userDB);
                await _context.SaveChangesAsync();
            }
            
        }

        public async Task<AppUser> GetByIdAsync(string id)
        {
            var user = await _context.Users.Include(l => l.Urls).FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<List<AppUser>> GetUsersAsync()
        {
            var allUsers = await _context.Users.Include(l => l.Urls).ToListAsync();
            return allUsers;
        }

        public async Task<AppUser> UpdateAsync(string id, AppUser user)
        {
            var userDB = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (userDB != null)
            {
                userDB.FullName = user.FullName;
                userDB.Email = user.Email;
                userDB.Urls = user.Urls;
                _context.Users.Update(userDB); //this line is optional as we are modifying the existing entity
                await _context.SaveChangesAsync();
            }
            return userDB;
        }
    }
}
