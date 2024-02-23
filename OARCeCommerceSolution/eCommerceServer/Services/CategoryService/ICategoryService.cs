namespace eCommerceServer.Services.CategoryService
{
    public interface ICategoryService
    {
        public Task<ServiceResponse<List<Category>>> GetCategoriesAsync();
        public Task<ServiceResponse<List<Category>>> GetAdminCategoriesAsync();
        public Task<ServiceResponse<Category>> GetCategoryAsync(int Id);
        public Task<ServiceResponse<List<Category>>> AddCategoryAsync(Category category);
        public Task<ServiceResponse<List<Category>>> UpdateCategoryAsync(Category category);
        public Task<ServiceResponse<List<Category>>> DeleteCategoryAsync(int Id);

    }
}
