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
                        new User
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
    }
}
