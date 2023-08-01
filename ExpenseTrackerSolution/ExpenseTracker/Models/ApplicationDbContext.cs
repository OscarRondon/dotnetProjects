using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Configuration;

namespace ExpenseTracker.Models
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(EnvConfigurations _envConfigurationService, DbContextOptions options):base(options)
        { 
        }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}
