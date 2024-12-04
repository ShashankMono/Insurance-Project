using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface IEmployeeService
    {
        Task<Guid> UpdateEmployeeProfile(EmployeeDto employee);
        Task<UserDto> AddAgent(AgentInputDto agent);
        Task<List<AgentInputDto>> GetAllAgents();
        Task<List<CommissionDto>> GetCommissions();
        Task<List<PolicyAccountDto>> GetAllPolicyAccounts();
        Task<List<ClaimDto>> GetClaimAccounts();
        Task<List<CommissionWithdrawalDto>> GetCommissionsWithdrawal();
        Task<List<PolicyCancelDto>> GetPolicyCancels();
        Task<List<TransactionDto>> GetTransactions();
        Task<List<CustomerDto>> GetCustomerAccounts();
        Task<Guid> ChangeApproveStatus(DocumentDto document);
        Task<AgentInputDto> GetAgentReport(AgentInputDto agent); // Includes all fields like commissions, commission withdrawal, policy account
        Task<PolicyAccountDto> GetPolicyAccount(PolicyAccountDto policyAccount);
        Task<List<PolicyDto>> GetPolicies();
        Task<PolicyDto> GetPolicy(Guid policyId);

    }
}
