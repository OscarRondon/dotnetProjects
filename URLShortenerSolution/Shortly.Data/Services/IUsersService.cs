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
        Task<List<User>> GetUsersAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> CreateAsync(User url);
        Task DeleteAsync(int id);
        Task<User> UpdateAsync(int id, User url);
    }
}
