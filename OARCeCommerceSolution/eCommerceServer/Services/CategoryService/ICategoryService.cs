namespace eCommerceServer.Services.CategoryService
{
    public interface ICategoryService
    {
        public Task<ServiceResponse<List<Category>>> GetCategoriesAsync();
        public Task<ServiceResponse<Category>> GetCategoryAsync(int Id);
    }
}
