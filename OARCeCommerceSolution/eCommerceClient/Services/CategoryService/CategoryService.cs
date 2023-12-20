
using System.Net.Http.Json;

namespace eCommerceClient.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly ClientAppSettings _settings;
        public CategoryService(HttpClient httpClient, ClientAppSettings settings)
        {
            _httpClient = httpClient;
            _settings = settings;
        }
        public List<Category> Categories { get; set; } = new List<Category>();

        public async Task GetCategoriesAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Category>>>(_settings.BackendApiURL + "category");
            if (result != null && result.Data != null)
                Categories = result.Data;
        }

        public async Task<ServiceResponse<Category>> GetCategoryAsync(int Id)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<Category>>(_settings.BackendApiURL + "category/" + Id.ToString());
            return result;
        }
    }
}
