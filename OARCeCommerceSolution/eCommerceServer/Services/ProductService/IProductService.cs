namespace eCommerceServer.Services.ProductService
{
    public interface IProductService
    {
        public Task<ServiceResponse<List<Product>>> GetProductsAsync();
        public Task<ServiceResponse<Product>> GetProductAsync(int Id);

        public Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(string categoryUrl);
        public Task<ServiceResponse<ProductSearchResult>> SearchProductsAsync(string searchText, int page);
        public Task<ServiceResponse<List<string>>> GetProdctsSearchSuggestionsAsync(string searchText);
        public Task<ServiceResponse<List<Product>>> GetFeaturedProductsAsync();
        public Task<ServiceResponse<List<Product>>> GetAdminProductsAsync();
    }
}
