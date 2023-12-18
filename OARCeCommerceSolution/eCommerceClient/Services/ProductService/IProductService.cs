namespace eCommerceClient.Services.ProductService
{
    public interface IProductService
    {
        public List<Product> Products { get; set;  }
        public Task GetProducts();
        public Task<ServiceResponse<Product>> GetProduct(int Id);
    }
}
