using Insurance_final_project.Dto;
using Insurance_final_project.Models;

namespace Insurance_final_project.Services
{
    public interface IAdminService
    {
        Task<UserDto> AddEmployee(EmployeeDto employee);
        Task<List<EmployeeDto>> GetAllEmployee(); // new added
        Task<UserDto> AddAgent(AgentDto agent);
        Task<List<AgentDto>> GetAllAgents(); // new added
        Task<Guid> AddCity(CityDto city);
        Task<Guid> UpdateCity(CityDto city);
        Task<Guid> AddState(StateDto state);
        Task<Guid> UpdateState(StateDto state);
        Task<List<CommissionDto>> GetCommissions();
        Task<List<PolicyAccountDto>> GetAllPolicyAccounts();
        Task<List<ClaimDto>> GetClaimAccounts();
        Task<Guid> ClaimApproval(ClaimDto claim);
        Task<Guid> DeActivateUser(ChangeUserStatusDto user);
        Task<List<CommissionWithdrawalDto>> GetCommissionsWithdrawal();
        Task<Guid> ApproveWithdrawal(CommissionWithdrawalDto withdrawRequest);
        Task<List<PolicyCancelDto>> GetPolicyCancels();
        Task<Guid> ApprovePolicyCancelation(PolicyCancelDto policyCancel);
        Task<List<TransactionDto>> GetTransactions();
        Task<Guid> AddPolicy(PolicyDto policy);
        Task<Guid> UpdatePolicy(PolicyDto policy);
        Task<List<PolicyDto>> GetPolicies(); // added new
        Task<PolicyDto> GetPolicy(Guid policyId); // added new
        Task<List<CustomerDto>> GetCustomerAccounts();
        Task<Guid> ApproveCustomer(CustomerDto customer);
        Task<AgentDto> GetAgentReport(AgentDto agent); // includes all fields like commissions, commission withdrawal, policy account
        Task<PolicyAccountDto> GetPolicyAccount(PolicyAccountDto policyAccount);
        Task<Guid> AddRole(RoleDto role);
        Task<Guid> AddPolicyType(PolicyTypeDto policyType);





    }
}
