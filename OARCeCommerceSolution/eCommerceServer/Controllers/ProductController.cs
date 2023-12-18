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
        public async Task<ActionResult<ServiceResponse<IEnumerable<Product>>>> GetProducts()
        {
            var result = await _productService.GetProductsAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("{Id:int}")]
        public async Task<ActionResult<ServiceResponse<Product>>> GetProduct(int Id)
        {
            var result = await _productService.GetProductAsync(Id);
            return Ok(result);
        }
    }
}
