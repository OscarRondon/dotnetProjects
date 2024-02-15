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
            _cartService = cartService;
            _authService = authService;
            _orderService = orderService;
            _configuration = configuration;
            StripeConfiguration.ApiKey = GetStripeSecretKey();
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

        public async Task<ServiceResponse<bool>> FullfillOrderAsync(HttpRequest request)
        {
            var json = await new StreamReader(request.Body).ReadToEndAsync();
            try
            { 
                var stripeEvent = EventUtility.ConstructEvent(json, request.Headers["Stripe-Signature"], GetStripeWebhookKey());
                if (stripeEvent.Type == Events.CheckoutSessionCompleted) 
                {
                    var session = stripeEvent.Data.Object as Session;
                    var user = await _authService.GetUserByEmailAsync(session.CustomerEmail);
                    await _orderService.PlaceOrderAsync(user.Id);
                }
            }
            catch (Exception ex) 
            {
                return new ServiceResponse<bool>
                { 
                    Success = false,
                    Message = ex.Message,
                    Data = false
                };
            }

            return new ServiceResponse<bool>
            {
                Success = true,
                Message = "Order Placed succesfully",
                Data = true
            };
        }

        private string GetStripeSecretKey()
        {
            // Get values from .env file
            string token = _configuration.GetSection("Stripe:SecretKey").Value;
            return token;
        }

        private string GetStripeWebhookKey()
        {
            // Get values from .env file
            string token = _configuration.GetSection("Stripe:WebhookKey").Value;
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
