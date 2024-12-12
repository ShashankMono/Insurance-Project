using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface IPolicyAccountService
    {
        public Task<PolicyAccountResponseDto> GetPolicyAccountById(Guid policyAccountId);
        public Task<List<PolicyAccountResponseDto>> GetAllPolicyAccounts();
        public Task<Guid> CreatePolicyAccount(PolicyAccountDto policyAccountDto);
        public Task<List<PolicyAccountResponseDto>> GetPolicyAccountsByAgent(Guid agentId);
        public Task<List<PolicyAccountResponseDto>> GetPoliciesByCustomer(Guid customerId);
        public Task<bool> ApproveAccount(ApprovalDto approval);
    }
}
