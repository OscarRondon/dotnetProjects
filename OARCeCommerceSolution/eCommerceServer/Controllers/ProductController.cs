using eCommerceServer.Services.ProductService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Product>>>> GetProductsAsync()
        {
            var result = await _productService.GetProductsAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("{Id:int}")]
        public async Task<ActionResult<ServiceResponse<Product>>> GetProductAsync(int Id)
        {
            var result = await _productService.GetProductAsync(Id);
            return Ok(result);
        }

        [HttpGet]
        [Route("category/{categoryUrl}")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Product>>>> GetProductsByCategoryAsync(string categoryUrl)
        {
            var result = await _productService.GetProductsByCategoryAsync(categoryUrl);
            return Ok(result);
        }
    }
}
