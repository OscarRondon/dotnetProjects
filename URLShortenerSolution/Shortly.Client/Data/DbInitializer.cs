using Microsoft.AspNetCore.Identity;
using Shortly.Data;
using Shortly.Data.Models;

namespace Shortly.Client.Data
{
    public static class DbInitializer
    {
        public static void SeedDefaultData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDBContext>();
                dbContext.Database.EnsureCreated();
                if (!dbContext.Users.Any())
                {
                    dbContext.Users.Add(
                        new AppUser
                        {
                            FullName = "Oscar Rondon",
                            Email = "oscar.rondon@email.com"
                        }

                        );
                    dbContext.SaveChanges();
                }

                if (!dbContext.Urls.Any())
                {
                    dbContext.Urls.Add(
                        new Url
                        {
                            OriginalLink = "www.google.com",
                            ShortLink = "wgc",
                            NrOfClicks = 0,
                            UserId = dbContext.Users.First().Id,
                            DateCreated = DateTime.Now,
                            DateUpdated = DateTime.Now
                        }

                        );
                    dbContext.SaveChanges();
                }
            }
        }

        public static async Task SeedDefaulUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                //Roles to be created
                string[] roleNames = { "Admin", "User" };
                // Check if roles exist, if not create them
                foreach (var roleName in roleNames)
                {
                    if (!await roleManager.RoleExistsAsync(roleName))
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName) { Name = roleName});
                    }
                }

                //simple user related data
                var simpleUserRole = "User";
                var simpleUser = new AppUser
                {
                    UserName = "simpleUser",
                    FullName = "Simple User",
                    Email = "user@user.com",
                    EmailConfirmed = true
                };

                if (await userManager.FindByEmailAsync(simpleUser.Email) == null)
                {
                    var result = await userManager.CreateAsync(simpleUser, "User.123");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(simpleUser, simpleUserRole);
                    }
                }

                //admin user related data
                var adminUserRole = "User";
                var adminUser = new AppUser
                {
                    UserName = "adminUser",
                    FullName = "Admin User",
                    Email = "admin@admin.com",
                    EmailConfirmed = true
                };

                if (await userManager.FindByEmailAsync(adminUser.Email) == null)
                {
                    var result = await userManager.CreateAsync(adminUser, "Admin.123");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, adminUserRole);
                    }
                }

            }
        }

    }
}
