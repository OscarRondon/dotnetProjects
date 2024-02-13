namespace eCommerceClient.Services.OrderService
{
    public interface IOrderService
    {
        public Task PlaceOrderAsync();
        public Task<string> PlaceOrderURLReturnAsync();
        public Task<List<OrderOverviewResponse>> GetOrderOverviewAsync();
        public Task<OrderDetailsResponse> GetOrderDetailsAsync(int orderId);
    }
}
