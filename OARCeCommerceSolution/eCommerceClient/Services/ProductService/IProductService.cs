namespace eCommerceClient.Services.ProductService
{
    public interface IProductService
    {
        event Action ProductChanged;
        public List<Product> Products { get; set;  }
        public Task GetProductsAsync(string categoryUrl = null);
        public Task<ServiceResponse<Product>> GetProductAsync(int Id);
    }
}
