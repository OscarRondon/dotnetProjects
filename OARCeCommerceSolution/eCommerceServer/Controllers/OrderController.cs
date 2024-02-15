using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        //Method replaceed for PaymentController FullfillOrderAsync
        //[HttpPost("Add")]
        //public async Task<ActionResult<ServiceResponse<bool>>> PlaceOrderAsync()
        //{
        //    var result = await _orderService.PlaceOrderAsync();
        //    return Ok(result);
        //}

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<OrderOverviewResponse>>>> GetOrdersAsync()
        {
            var result = await _orderService.GetOrdersAsync();
            return Ok(result);
        }

        [HttpGet("{orderId:int}")]
        public async Task<ActionResult<ServiceResponse<OrderDetailsResponse>>> GetOrderDetailsAsync(int orderId)
        {
            var result = await _orderService.GetOrderDetailsAsync(orderId);
            return Ok(result);
        }
    }
}
