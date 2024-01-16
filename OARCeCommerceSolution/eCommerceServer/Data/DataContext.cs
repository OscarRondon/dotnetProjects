
namespace eCommerceServer.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductVariant>()
                .HasKey(p => new { p.ProductId, p.ProductTypeId });

            modelBuilder.Entity<ProductType>().HasData(DataSeed.ProductTypes);
            modelBuilder.Entity<Product>().HasData(DataSeed.Products);
            modelBuilder.Entity<Category>().HasData(DataSeed.Categories);
            modelBuilder.Entity<ProductVariant>().HasData(DataSeed.ProductVariants);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
