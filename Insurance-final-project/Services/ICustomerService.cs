using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface ICustomerService
    {
        public Guid CreatePolicyAccount(PolicyAccountDto policyAccountDto);
        public void AddInstallments(Guid policyId, DateTime startDate, int years, string choice, double totalAmount);
        public bool CancelPolicy(Guid policyAccountId);
        public bool ClaimPolicy(Guid policyAccountId, ClaimDto claimDto);

        public void SubmitQuery(QueryDto queryDto);
        public CustomerDto GetCustomerById(Guid customerId);
        public bool UpdateProfile(CustomerDto customerDto);
        public ICollection<PolicyDto> GetAvailablePolicies();
        public CustomerDto RegisterCustomer(CustomerDto customerDto);
        public ICollection<PolicyAccountDto> GetPoliciesByCustomer(Guid customerId);
        //public void BuyPolicy(Guid customerId, PolicyAccountDto policyAccountDto, PolicyDto policyDto);
    }

}
