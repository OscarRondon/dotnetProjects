using Stripe;
using Stripe.Checkout;

namespace eCommerceServer.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly ICartService _cartService;
        private readonly IAuthService _authService;
        private readonly IOrderService _orderService;
        private readonly IConfiguration _configuration;

        public PaymentService(
            ICartService cartService,
            IAuthService authService,
            IOrderService orderService,
            IConfiguration configuration
            ) 
        {
            StripeConfiguration.ApiKey = GetStripeSecretKey();
            _cartService = cartService;
            _authService = authService;
            _orderService = orderService;
            _configuration = configuration;
        }
        public async Task<Session> CreateCheckoutSession()
        {
            var products = (await _cartService.GetDBCartProductsAsync()).Data;
            var lineItems = new List<SessionLineItemOptions>();
            products.ForEach(product => lineItems.Add(new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions 
                {
                    UnitAmountDecimal = product.Price*100,
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = product.Tittle,
                        Images = new List<string> { product.ImageUrl }
                    }
                },
                Quantity = product.Quantity,
            }));

            var options = new SessionCreateOptions
            {
                CustomerEmail = _authService.GetUserEmail(),
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = GetStripeSuccessUrl(),
                CancelUrl = GetStripeCancelUrl()
            };

            var service = new SessionService();
            Session session = service.Create(options);
            return session;
        }
        private string GetStripeSecretKey()
        {
            // Get values from .env file
            string token = _configuration.GetSection("Stripe:SecretKey").Value;
            return token;
        }

        private string GetStripeSuccessUrl()
        {
            // Get values from .env file
            string token = _configuration.GetSection("Stripe:SuccessUrl").Value;
            return token;
        }

        private string GetStripeCancelUrl()
        {
            // Get values from .env file
            string token = _configuration.GetSection("Stripe:CancelUrl").Value;
            return token;
        }
    }
}
