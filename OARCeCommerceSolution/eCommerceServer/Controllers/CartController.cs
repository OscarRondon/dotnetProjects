﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eCommerceServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }


        [HttpPost("Products")]
        public async Task<ActionResult<ServiceResponse<List<CartProductResponse>>>> GetCartProductsAsync(List<CartItem> cartItems)
        {
            var result = await _cartService.GetCartProductsAsync(cartItems);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<CartProductResponse>>>> StoreCartItemsAsync(List<CartItem> cartItems)
        {
            //var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)); <-- To access the loged user info without http content accesor
            var result = await _cartService.StoreCartItemsAsync(cartItems/*, userId*/);
            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<ActionResult<ServiceResponse<bool>>> AddToCartAsync(CartItem cartItems)
        {
            var result = await _cartService.AddToCartAsync(cartItems);
            return Ok(result);
        }

        [HttpPut("Update-Quantity")]
        public async Task<ActionResult<ServiceResponse<bool>>> UpdateQuantityAsync(CartItem cartItems)
        {
            var result = await _cartService.UpdateQuantityAsync(cartItems);
            return Ok(result);
        }

        [HttpDelete("{productId}/{productTypeId}")]
        public async Task<ActionResult<ServiceResponse<bool>>> RemoveItemFromCartAsync(int productId, int productTypeId)
        {
            var result = await _cartService.RemoveItemFromCartAsync(productId, productTypeId);
            return Ok(result);
        }

        [HttpGet("CartItems-Count")]
        public async Task<ActionResult<ServiceResponse<int>>> GetCartItemsCountAsync()
        {
            return await _cartService.GetCartItemsCountAsync();
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<CartProductResponse>>>> GetDBCartProductsAsync()
        {
            var result = await _cartService.GetDBCartProductsAsync();
            return Ok(result);
        }


    }
}
