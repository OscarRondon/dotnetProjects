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
        Task<List<AppUser>> GetUsersAsync();
        Task<AppUser> GetByIdAsync(int id);
        Task<AppUser> CreateAsync(AppUser url);
        Task DeleteAsync(int id);
        Task<AppUser> UpdateAsync(int id, AppUser url);
    }
}
