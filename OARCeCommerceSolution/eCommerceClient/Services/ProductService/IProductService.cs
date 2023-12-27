namespace eCommerceClient.Services.ProductService
{
    public interface IProductService
    {
        public event Action ProductChanged;
        public List<Product> Products { get; set;  }
        public string Message { get; set; }
        public Task GetProductsAsync(string categoryUrl = null);
        public Task<ServiceResponse<Product>> GetProductAsync(int Id);
        public Task SearchProductsAsync(string searchText);
        public Task<List<string>> GetProductSearchSuggestionsAsync(string searchText);
    }
}
