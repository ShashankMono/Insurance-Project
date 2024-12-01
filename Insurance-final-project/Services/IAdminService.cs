using Insurance_final_project.Dto;
using Insurance_final_project.Models;

namespace Insurance_final_project.Services
{
    public interface IAdminService
    {
        //public User AddUser(UserDto userDto);
        public Guid AddEmployee(EmployeeDto employee);
        public Guid AddAgent(AgentDto agent);
        public Guid City(CityDto city);
        public Guid State(StateDto state);
        public List<CommissionDto> GetCommissions();
        public List<PolicyAccountDto> GetPolicyAccounts();
        public List<ClaimDto> GetClaimAccounts();
        public Guid ClaimApproval(ClaimDto claim);
        public Guid DeActivateUser(UserDto user);
        public List<CommissionWithdrawalDto> GetCommissionsWithdrawal();
        public Guid ApproveWithdrawal(CommissionWithdrawalDto withdrawRequest);
        public List<PolicyCancelDto> GetPolicyCancels();
        public Guid ApprovePolicyCancelation(PolicyCancelDto policyCancel);
        public List<TransactionDto> GetTransactions();
        public Guid AddPolicy(PolicyDTO policy);
        public Guid UpdatePolicy(PolicyDTO policy);
        public List<CustomerDto> GetCustomerAccounts();
        public Guid ApproveCustomer(CustomerDto customer);
        public AgentDto GetAgentReport(AgentDto agent);// incliudes all fields like commissions, commission withdrwal, policy account
        public PolicyAccountDto GetPolicyAccount(PolicyAccountDto policyAccount);
        public Guid AddRole(RoleDto role);
        public Guid AddPolicyType(PolicyTypeDto policyType);


        


    }
}
