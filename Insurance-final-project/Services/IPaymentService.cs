using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface IPaymentService
    {
        public Task<string> CreatePaymentSession(PaymentDto payment);
    }  
}
