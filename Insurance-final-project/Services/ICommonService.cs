using Insurance_final_project.Constant;
using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface ICommonService
    {
        public ICollection<CityDto> GetCities();
        public ICollection<StateDto> GetStates();
        public List<PolicyDto> GetPolicies();
        public List<RoleDto> GetRoles();
        public List<string> GetapprovalTypes();
        public List<string> GetPolicyAccountStatus();
        public List<string> GetTransactionStatus();
        public List<string> GetVerificationType();
    }
}
