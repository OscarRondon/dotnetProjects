namespace eCommerceClient.Services.CategoryService
{
    public interface ICategoryService
    {
        event Action OnChange;
        public List<Category> Categories { get; set; }
        public List<Category> AdminCategories { get; set; }
        public Task GetCategoriesAsync();
        public Task<ServiceResponse<Category>> GetCategoryAsync(int Id);
        public Task GetAdminCategoriesAsync();
        public Task AddCategoryAsync(Category category);
        public Task UpdateCategoryAsync(Category category);
        public Task DeleteCategoryAsync(int Id);

        public Category CreateNewCategory();
    }
}
