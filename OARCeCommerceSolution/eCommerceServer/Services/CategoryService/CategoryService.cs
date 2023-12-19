
namespace eCommerceServer.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _context;

        public CategoryService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Category>> GetCategoryAsync(int Id)
        {
            var response = new ServiceResponse<Category>();
            var category = await _context.Categories.FindAsync(Id);
            if (category == null)
            {
                response.Success = false;
                response.Message = "Category not found";
                response.Data = null;
            }
            else
                response.Data = category;
            return response;

        }

        public async Task<ServiceResponse<List<Category>>> GetCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            var response = new ServiceResponse<List<Category>>() { Data = categories };
            return response;
        }

        
    }
}
