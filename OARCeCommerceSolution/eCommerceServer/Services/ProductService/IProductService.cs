namespace eCommerceServer.Services.ProductService
{
    public interface IProductService
    {
        public Task<ServiceResponse<List<Product>>> GetProductsAsync();
        public Task<ServiceResponse<Product>> GetProductAsync(int Id);

        public Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(string categoryUrl);
    }
}
