using Insurance_final_project.Dto;
using Insurance_final_project.Models;

namespace Insurance_final_project.Services
{
    public interface IAdminService
    {
        Task<UserDto> AddEmployee(EmployeeDto employee);
        Task<List<EmployeeDto>> GetAllEmployee(); // new added
        Task<UserDto> AddAgent(AgentInputDto agent);
        Task<List<AgentInputDto>> GetAllAgents(); // new added
        Task<Guid> AddCity(CityInputDto city);
        Task<Guid> UpdateCity(CityInputDto city);
        Task<Guid> AddState(StateDto state);
        Task<Guid> UpdateState(StateDto state);
        Task<List<CommissionDto>> GetCommissions();
        Task<List<PolicyAccountDto>> GetAllPolicyAccounts();
        Task<List<ClaimDto>> GetClaimAccounts();
        Task<Guid> ClaimApproval(ApprovalDto claim);
        Task<Guid> DeActivateUser(ChangeUserStatusDto user);
        Task<List<CommissionWithdrawalDto>> GetCommissionsWithdrawal();
        Task<Guid> ApproveWithdrawal(CommissionWithdrawalDto withdrawRequest);
        Task<List<PolicyCancelDto>> GetPolicyCancels();
        Task<Guid> ApprovePolicyCancelation(ApprovalDto policyCancel);
        Task<List<TransactionDto>> GetTransactions();
        Task<Guid> AddPolicy(PolicyDto policy);
        Task<Guid> UpdatePolicy(PolicyDto policy);
        Task<List<PolicyDto>> GetPolicies(); // added new
        Task<PolicyDto> GetPolicy(Guid policyId); // added new
        Task<List<CustomerDto>> GetCustomerAccounts();
        Task<Guid> ApproveCustomer(CustomerDto customer);
        Task<AgentResponseDto> GetAgentReport(AgentInputDto agent); // includes all fields like commissions, commission withdrawal, policy account
        Task<PolicyAccountDto> GetPolicyAccount(PolicyAccountDto policyAccount);
        Task<Guid> AddRole(RoleDto role);
        Task<Guid> AddPolicyType(PolicyTypeDto policyType);





    }
}
