using Insurance_final_project.Constant;
using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface ICommonService
    {
        public Task<List<CityDto>> GetCities();
        public Task<List<StateDto>> GetStates();
        public Task<List<PolicyDto>> GetPolicies();
        public Task<List<RoleDto>> GetRoles();
        public Task<List<string>> GetapprovalTypes();
        public Task<List<string>> GetPolicyAccountStatus();
        public Task<List<string>> GetTransactionStatus();
        public Task<List<string>> GetVerificationType();
        public Task<List<PolicyTypeDto>> GetPolicyType();
        public Task<List<string>> GetpolicyInstallmentType();
    }
}
