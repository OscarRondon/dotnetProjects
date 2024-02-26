
using eCommerceShared;
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
        public List<Category> AdminCategories { get; set; } = new List<Category>();

        public event Action OnChange;

        public async Task AddCategoryAsync(Category category)
        {
            var response = await _httpClient.PostAsJsonAsync(_settings.BackendApiURL + "Category/Admin", category);
            AdminCategories = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<Category>>>()).Data;
            await GetCategoriesAsync();
            OnChange.Invoke();
        }

        public Category CreateNewCategory()
        {
            var newCategory = new Category
            {
                IsNew = true,
                Editing = true,
            };
            AdminCategories.Add(newCategory);
            OnChange.Invoke();
            return newCategory;
        }

        public async Task DeleteCategoryAsync(int Id)
        {
            var response = await _httpClient.DeleteAsync(_settings.BackendApiURL + "Category/Admin/" + Id);
            AdminCategories = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<Category>>>()).Data;
            await GetCategoriesAsync();
            OnChange.Invoke(); ;
        }

        public async Task GetAdminCategoriesAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Category>>>(_settings.BackendApiURL + "Category/Admin");
            if (result != null && result.Data != null)
                AdminCategories = result.Data;
        }

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

        public async Task UpdateCategoryAsync(Category category)
        {
            var response = await _httpClient.PutAsJsonAsync(_settings.BackendApiURL + "Category/Admin", category);
            AdminCategories = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<Category>>>()).Data;
            await GetCategoriesAsync();
            OnChange.Invoke();
        }
    }
}
