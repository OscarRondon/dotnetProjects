using eCommerceClient.Pages.Admin;
using eCommerceShared;
using System.Net.Http.Json;

namespace eCommerceClient.Services.ProductTypeService
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly HttpClient _httpClient;
        private readonly ClientAppSettings _settings;

        public ProductTypeService(HttpClient httpClient, ClientAppSettings settings)
        {
            _httpClient = httpClient;
            _settings = settings;
        }


        public event Action OnChange;
        public List<ProductType> ProductTypes { get; set; } = new List<ProductType>();

        public async Task GetProductTypesAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<ProductType>>>(_settings.BackendApiURL + "ProductType");
            if (result != null && result.Data != null)
                ProductTypes = result.Data;
        }

        public async Task AddProductTypesAsync(ProductType productType)
        {
            var response = await _httpClient.PostAsJsonAsync(_settings.BackendApiURL + "ProductType", productType);
            ProductTypes = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<ProductType>>>()).Data;
            await GetProductTypesAsync();
            OnChange.Invoke();
        }

        public async Task UpdateProductTypesAsync(ProductType productType)
        {
            var response = await _httpClient.PutAsJsonAsync(_settings.BackendApiURL + "ProductType", productType);
            ProductTypes = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<ProductType>>>()).Data;
            await GetProductTypesAsync();
            OnChange.Invoke();
        }

        public ProductType CreateNewProductType()
        {
            var productType = new ProductType
            {
                Editing = true,
                IsNew = true,
            };
            ProductTypes.Add(productType);
            OnChange.Invoke();
            return productType;
        }
    }
}
