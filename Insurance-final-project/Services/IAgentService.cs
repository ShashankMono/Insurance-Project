using Insurance_final_project.Dto;
using Insurance_final_project.Models;

namespace Insurance_final_project.Services
{
    public interface IAgentService
    {
        public AgentDto GetAgentById(Guid agentId);
        
        public ICollection<CommissionWithdrawalDto> GetCommissionWithdrawals(Guid agentId);
        public void WithdrawCommission(Guid agentId, double amount);
        public double ViewTotalCommission(Guid agentId);
        
        public ICollection<PolicyAccountDto> GetPolicyAccountsByAgent(Guid agentId);
    }
}
