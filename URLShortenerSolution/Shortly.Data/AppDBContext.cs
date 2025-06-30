using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shortly.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Data
{
    public class AppDBContext: IdentityDbContext<AppUser>
    {
        public AppDBContext(DbContextOptions<AppDBContext> dbContextOptions): base(dbContextOptions)
        {

        }

        public DbSet<Url> Urls { get; set; } = null!;
    }
}
