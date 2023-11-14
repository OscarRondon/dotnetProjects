using eCommerceTickets.Data;
using eCommerceTicketsTest.InMemory.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceTicketsTest.InMemory.Utilities
{
    public class AppDbContextFixture: IDisposable
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "eCommerceTicketsDBTest").Options;
        public AppDbContext appDbContext;

        public AppDbContextFixture()
        {
            appDbContext = new AppDbContext(dbContextOptions);
            appDbContext.Database.EnsureCreated();
            AppDbInitializerTest.Seed(appDbContext);
        }

        public void Dispose()
        {
            appDbContext.Database.EnsureDeleted();
        }
    }
}
