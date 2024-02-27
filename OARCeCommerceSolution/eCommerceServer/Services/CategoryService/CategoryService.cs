
using eCommerceShared;

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
            var categories = await _context.Categories
                .Where(c => c.Visible && !c.Deleted)
                .ToListAsync();
            var response = new ServiceResponse<List<Category>>() { Data = categories };
            return response;
        }

        public async Task<ServiceResponse<List<Category>>> GetAdminCategoriesAsync()
        {
            var categories = await _context.Categories
                 .ToListAsync();
            var response = new ServiceResponse<List<Category>>() { Data = categories };
            return response;
        }

        public async Task<ServiceResponse<List<Category>>> AddCategoryAsync(Category category)
        {
            category.Editing = false;
            category.IsNew = false;
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return await  GetAdminCategoriesAsync();
        }

        public async Task<ServiceResponse<List<Category>>> UpdateCategoryAsync(Category category)
        {
            var dbCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);
            if (dbCategory == null)
            {
                return new ServiceResponse<List<Category>> { Success = false, Message = "Category not found.", Data = null};
            }
            dbCategory.Name = category.Name;
            dbCategory.Url = category.Url;
            dbCategory.IconStyle = category.IconStyle;
            dbCategory.Visible = category.Visible;
            dbCategory.Deleted = false;
            
            await _context.SaveChangesAsync();
            return await GetAdminCategoriesAsync();
        }

        public async Task<ServiceResponse<List<Category>>> DeleteCategoryAsync(int Id)
        {
            var dbCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == Id);
            if (dbCategory == null)
            {
                return new ServiceResponse<List<Category>> { Success = false, Message = "Category not found.", Data = null };
            }
            dbCategory.Deleted = true;

            await _context.SaveChangesAsync();
            return await GetAdminCategoriesAsync();
        }
    }
}
