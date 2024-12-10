
using Insurance_final_project.Dto;
using Stripe;
using Stripe.BillingPortal;
using Stripe.Checkout;
using SessionCreateOptions = Stripe.Checkout.SessionCreateOptions;
using SessionService = Stripe.Checkout.SessionService;
using StripeCustomerService = Stripe.CustomerService;



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

            var customerOptions = new CustomerCreateOptions
            {
                Name = "Test Customer", // Dummy name
                Address = new AddressOptions
                {
                    Line1 = "123 Default Street",
                    City = "Mumbai",
                    State = "Maharashtra",
                    PostalCode = "400001",
                    Country = "IN" // Must be valid ISO country code
                }
            };
            var customerService = new StripeCustomerService();
            var customer = customerService.Create(customerOptions);


            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "inr",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = paymentDto.PolicyName,
                            },
                            UnitAmount = (int)Math.Round(paymentDto.Amount * 100),
                        },
                        Quantity = 1
                    },
                },


                Customer =customer.Id,
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
