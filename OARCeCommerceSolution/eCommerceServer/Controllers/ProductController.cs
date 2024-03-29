﻿using eCommerceServer.Services.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<Product>>> CreateProductAsync(Product product)
        {
            var result = await _productService.CreateProductAsync(product);
            return Ok(result);
        }

        [HttpPut, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<Product>>> UpdateProductAsync(Product product)
        {
            var result = await _productService.UpdateProductAsync(product);
            return Ok(result);
        }

        [HttpDelete("{Id:int}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteProductAsync(int Id)
        {
            var result =  await _productService.DeleteProductAsync(Id);
            return Ok(result);
        }

        [HttpGet("Admin")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Product>>>> GetAdminProductsAsync()
        {
            var result = await _productService.GetAdminProductsAsync();
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
        [Route("Category/{categoryUrl}")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Product>>>> GetProductsByCategoryAsync(string categoryUrl)
        {
            var result = await _productService.GetProductsByCategoryAsync(categoryUrl);
            return Ok(result);
        }

        [HttpGet]
        [Route("Search/{searchText}/{page}")]
        public async Task<ActionResult<ServiceResponse<ProductSearchResult>>> SearchProductsAsync(string searchText, int page = 1)
        {
            var result = await _productService.SearchProductsAsync(searchText, page);
            return Ok(result);
        }

        [HttpGet]
        [Route("SearchSuggestions/{searchText}")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Product>>>> GetProdctsSearchSuggestionsAsync(string searchText)
        {
            var result = await _productService.GetProdctsSearchSuggestionsAsync(searchText);
            return Ok(result);
        }

        [HttpGet]
        [Route("Featured")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Product>>>> GetFeaturedProductsAsync()
        {
            var result = await _productService.GetFeaturedProductsAsync();
            return Ok(result);
        }
    }
}
