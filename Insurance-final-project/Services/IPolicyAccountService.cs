using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface IPolicyAccountService
    {
        public Task<PolicyAccountDto> GetPolicyAccountById(Guid policyAccountId);
        public Task<List<PolicyAccountDto>> GetAllPolicyAccounts();
        public Task<Guid> CreatePolicyAccount(PolicyAccountDto policyAccountDto);
        public Task<List<PolicyAccountDto>> GetPolicyAccountsByAgent(Guid agentId);
        public Task<List<PolicyAccountDto>> GetPoliciesByCustomer(Guid customerId);
    }
}
