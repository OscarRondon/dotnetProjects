using eCommerceServer.Services.CategoryService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Category>>>> GetCategories()
        {
            var result = await _categoryService.GetCategoriesAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("{Id:int}")]
        public async Task<ActionResult<ServiceResponse<Product>>> GetProduct(int Id)
        {
            var result = await _categoryService.GetCategoryAsync(Id);
            return Ok(result);
        }
    }
}
