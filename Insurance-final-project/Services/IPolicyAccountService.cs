using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface IPolicyAccountService
    {
        public List<PolicyAccountDto> GetPolicyAccounts();
        public Guid AddPolicyAccount(PolicyAccountDto policyAccount);
        public bool UpdatePolicyAccountStatus(string status);
        public PolicyAccountDto GetPolicyAccount(Guid id);
    }
}
