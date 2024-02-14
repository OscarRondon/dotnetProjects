using Stripe.Checkout;

namespace eCommerceServer.Services.PaymentService
{
    public interface IPaymentService
    {
        public Task<Session> CreateCheckoutSession();
    }
}
