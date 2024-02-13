namespace eCommerceServer.Services.OrderService
{
    public interface IOrderService
    {
        public Task<ServiceResponse<bool>> PlaceOrderAsync();
        public Task<ServiceResponse<List<OrderOverviewResponse>>> GetOrdersAsync();
        public Task<ServiceResponse<OrderDetailsResponse>> GetOrderDetailsAsync(int orderId);
    }
}
