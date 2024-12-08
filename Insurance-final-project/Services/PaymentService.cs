
using Insurance_final_project.Dto;
using Stripe;
using Stripe.BillingPortal;
using Stripe.Checkout;
using SessionCreateOptions = Stripe.Checkout.SessionCreateOptions;
using SessionService = Stripe.Checkout.SessionService;



namespace Insurance_final_project.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        public PaymentService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> CreatePaymentSession(PaymentDto paymentDto)
        {
            StripeConfiguration.ApiKey = _configuration.GetValue<string>("StripeSettings:ApiKey");
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = paymentDto.PolicyName,
                        },
                        UnitAmount = paymentDto.Amount,
                    },
                    Quantity = 1
                }
            },
                Mode = "payment",
                SuccessUrl = paymentDto.SuccessUrl,
                CancelUrl = paymentDto.CancelUrl,
            };


            var service = new SessionService();
            var session = service.Create(options);
            return session.Url;
        }
    }
}
