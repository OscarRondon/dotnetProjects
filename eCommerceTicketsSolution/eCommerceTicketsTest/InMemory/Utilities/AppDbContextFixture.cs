using eCommerceTickets.Data;
using eCommerceTickets.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace eCommerceTicketsTest.InMemory.Utilities
{
    public class AppDbContextFixture : IDisposable
    {
        //DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "eCommerceTicketsDBTest").Options;
        public AppDbContext appDbContextTest;
        public UserManager<ApplicationUser> userManagerTest;
        public RoleManager<IdentityRole> roleManagerTest;

        public AppDbContextFixture()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddLogging(login => login.AddConsole());

            serviceCollection.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase(databaseName: "eCommerceTicketsDBTest");
            });
            appDbContextTest = serviceCollection.BuildServiceProvider().GetService<AppDbContext>();
            appDbContextTest.Database.EnsureCreated();

            
            
            serviceCollection.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            })
                .AddUserManager<UserManager<ApplicationUser>>()
                .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            userManagerTest = serviceCollection.BuildServiceProvider().GetService<UserManager<ApplicationUser>>();
            roleManagerTest = serviceCollection.BuildServiceProvider().GetService<RoleManager<IdentityRole>>();


            AppDbInitializer.Seed(serviceCollection.BuildServiceProvider());
            AppDbInitializer.SeedUsersAndRoles(serviceCollection.BuildServiceProvider()).Wait();
        }

        public void Dispose()
        {
            appDbContextTest.Database.EnsureDeleted();
            appDbContextTest.Dispose();
            userManagerTest.Dispose();
            roleManagerTest.Dispose();
        }
    }
}
