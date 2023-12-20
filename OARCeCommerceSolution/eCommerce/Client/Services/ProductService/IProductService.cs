namespace eCommerce.Client.Services.ProductService
{
    public interface IProductService
    {
        public List<Product> Products { get; set;  }
        public Task GetProductsAsync();
    }
}
