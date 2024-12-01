using Insurance_final_project.Dto;
using Insurance_final_project.Models;

namespace Insurance_final_project.Services
{
    public interface IAgentService
    {
        public AgentDto RegisterAgent(AgentDto agentDto);
        public AgentDto GetAgentById(Guid agentId);
        //public ICollection<CustomerDto> GetCustomersByAgent(Guid agentId);
        public void RecommendPlan(Guid customerId, Guid policyId, Guid agentId, PolicyAccountDto policyAccountDto);
        public ICollection<CommissionWithdrawalDto> GetCommissionWithdrawals(Guid agentId);
        public void WithdrawCommission(Guid agentId, double amount);
        public double ViewTotalCommission(Guid agentId);
        //public ICollection<ClaimDto> ViewCustomerClaims(Guid agentId);
    }
}
