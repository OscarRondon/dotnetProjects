
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
        public List<Product> AdminProducts { get; set; } = new List<Product>();
        public string Message { get; set; } = "Loading products...";
        public int CurrentPage { get; set; } = 1;
        public int PageCount { get; set; } = 0;
        public string LastSearchText {  get; set; } = string.Empty;

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

            CurrentPage = 1;
            PageCount = 0;

            if (Products.Count == 0)
                Message = "No products found.";

            ProductChanged.Invoke();
        }

        public async Task SearchProductsAsync(string searchText, int page)
        {
            LastSearchText = searchText;
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<ProductSearchResult>>(_settings.BackendApiURL + $"Product/Search/{searchText}/{page}");
            if(result != null && result.Data != null)
            {
                Products = result.Data.Products;
                CurrentPage = result.Data.CurrentPage;
                PageCount = result.Data.Pages;
            }
            if (Products.Count == 0)
                Message = "No products found.";
            ProductChanged?.Invoke();
        }

        public async Task<List<string>> GetProductSearchSuggestionsAsync(string searchText)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<string>>>(_settings.BackendApiURL + "Product/SearchSuggestions/" + searchText);
            return result.Data;
        }

        public async Task GetAdminProductsAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>(_settings.BackendApiURL + "Product/Admin");

            if (result != null && result.Data != null)
                AdminProducts = result.Data;

            CurrentPage = 1;
            PageCount = 0;

            if (AdminProducts.Count == 0)
                Message = "No products found.";
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            var result = await _httpClient.PostAsJsonAsync(_settings.BackendApiURL + "Product/", product);
            return (await result.Content.ReadFromJsonAsync<ServiceResponse<Product>>()).Data;
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            var result = await _httpClient.PutAsJsonAsync(_settings.BackendApiURL + "Product/", product);
            return (await result.Content.ReadFromJsonAsync<ServiceResponse<Product>>()).Data;
        }

        public async Task DeleteProductAsync(Product product)
        {
            var result = await _httpClient.DeleteAsync(_settings.BackendApiURL + "Product/" + product.Id);
        }
    }
}
