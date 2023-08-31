using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using SQLiteWebApi.Model;
using System.Formats.Asn1;
using System.Globalization;

namespace SQLiteWebApi.Data
{
    public class SQLiteWebApiDbContext: DbContext
    {
        public DbSet<AppUser> AppUser => Set<AppUser>();

        public SQLiteWebApiDbContext(DbContextOptions<SQLiteWebApiDbContext> options) : base(options) 
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AppUser>().HasData(GetAppUsers());
        }

        private static IEnumerable<AppUser> GetAppUsers()
        {
            string[] p = { Directory.GetCurrentDirectory(), "wwwroot", "AppUserInitLoad.csv" };
            var csvFilePath = Path.Combine(p);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower(),
            };

            var data = new List<AppUser>().AsEnumerable();
            using (var reader = new StreamReader(csvFilePath))
            {
                using (var csvReader = new CsvReader(reader, config))
                {
                    data = (csvReader.GetRecords<AppUser>()).ToList();
                }
            }

            return data;
        }
    }
}
