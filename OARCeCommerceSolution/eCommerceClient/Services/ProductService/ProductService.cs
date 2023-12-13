
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
        public async Task GetProducts()
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>(_settings.BackendApiURL+"product");
            if (result != null && result.Data != null)
                Products = result.Data;
        }
    }
}
