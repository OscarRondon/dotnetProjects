
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

        public event Action ProductChanged;

        public async Task<ServiceResponse<Product>> GetProductAsync(int Id)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<Product>>(_settings.BackendApiURL + "Product/" + Id.ToString());
            return result;
        }

        public async Task GetProductsAsync(string categoryUrl)
        {
            var result = categoryUrl == null ?
                await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>(_settings.BackendApiURL + "Product") :
                await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>(_settings.BackendApiURL + "Product/Category/" + categoryUrl);

            if (result != null && result.Data != null)
                Products = result.Data;

            ProductChanged.Invoke();
        }
    }
}
