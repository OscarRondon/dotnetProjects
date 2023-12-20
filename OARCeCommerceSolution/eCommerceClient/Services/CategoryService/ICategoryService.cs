namespace eCommerceClient.Services.CategoryService
{
    public interface ICategoryService
    {
        public List<Category> Categories { get; set; }
        public Task GetCategoriesAsync();
        public Task<ServiceResponse<Category>> GetCategoryAsync(int Id);
    }
}
