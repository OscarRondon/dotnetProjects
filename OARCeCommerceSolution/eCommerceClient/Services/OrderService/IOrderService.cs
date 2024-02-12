namespace eCommerceClient.Services.OrderService
{
    public interface IOrderService
    {
        public Task PlaceOrderAsync();
        public Task<List<OrderOverviewResponse>> GetOrderOverviewAsync();
    }
}
