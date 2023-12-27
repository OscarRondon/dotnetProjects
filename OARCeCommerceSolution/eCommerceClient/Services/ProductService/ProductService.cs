
using eCommerceShared;
using System.Net.Http.Json;

namespace eCommerceClient.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly ClientAppSettings _settings;

        public ProductService(HttpClient httpClient, ClientAppSettings settings)
        {
            _httpClient = httpClient;
            _settings = settings;
        }


        public List<Product> Products { get; set; } = new List<Product>();
        public string Message { get; set; } = "Loading products...";

        public event Action ProductChanged;

        public async Task<ServiceResponse<Product>> GetProductAsync(int Id)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<Product>>(_settings.BackendApiURL + "Product/" + Id.ToString());
            return result;
        }

        public async Task GetProductsAsync(string? categoryUrl = null)
        {
            var result = categoryUrl == null ?
                await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>(_settings.BackendApiURL + "Product/Featured") :
                await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>(_settings.BackendApiURL + "Product/Category/" + categoryUrl);

            if (result != null && result.Data != null)
                Products = result.Data;

            ProductChanged.Invoke();
        }

        public async Task SearchProductsAsync(string searchText)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>(_settings.BackendApiURL + "Product/Search/" + searchText);
            if(result != null && result.Data != null) 
                Products = result.Data;
            if (Products.Count == 0)
                Message = "No products found.";
            ProductChanged?.Invoke();
        }

        public async Task<List<string>> GetProductSearchSuggestionsAsync(string searchText)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<string>>>(_settings.BackendApiURL + "Product/SearchSuggestions/" + searchText);
            return result.Data;
        }

        
    }
}
