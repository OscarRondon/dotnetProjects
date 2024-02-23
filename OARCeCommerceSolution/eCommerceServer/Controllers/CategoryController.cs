using eCommerceServer.Services.CategoryService;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("Admin"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Category>>>> GetAdminCategoriesAsync()
        {
            var result = await _categoryService.GetAdminCategoriesAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("{Id:int}")]
        public async Task<ActionResult<ServiceResponse<Product>>> GetProduct(int Id)
        {
            var result = await _categoryService.GetCategoryAsync(Id);
            return Ok(result);
        }

        [HttpPost("Admin"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Category>>>> AddCategoryAsync(Category category)
        {
            var result = await _categoryService.AddCategoryAsync(category);
            return Ok(result);
        }

        [HttpPut("Admin"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Category>>>> UpdateCategoryAsync(Category category)
        {
            var result = await _categoryService.UpdateCategoryAsync(category);
            return Ok(result);
        }

        [HttpDelete("Admin"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Category>>>> DeleteCategoryAsync(int Id)
        {
            var result = await _categoryService.DeleteCategoryAsync(Id);
            return Ok(result);
        }
    }
}
