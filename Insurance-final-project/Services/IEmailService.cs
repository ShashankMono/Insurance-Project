using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface IEmailService
    {
        public void SendUserDetailthroughEmail(string toEmail, string subject,UserDto userDets);
        public void SendMarketingMail(MarketingDto info);
        public void RejectionMail(Guid customerId, string reason, string subject);
        public void CommissionWithdrawalMail(Guid agentId, double amount);
    }
}
