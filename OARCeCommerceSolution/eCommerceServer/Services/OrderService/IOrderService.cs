namespace eCommerceServer.Services.OrderService
{
    public interface IOrderService
    {
        public Task<ServiceResponse<bool>> PlaceOrderAsync();
    }
}
