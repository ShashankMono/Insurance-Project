using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface IPolicyService
    {
        public Task<List<PolicyDto>> GetPolicies();
        public Task<Guid> AddPolicy(PolicyDto policy);
        public Task<Guid> UpdatePolicy(PolicyDto policy);
        public Task<PolicyDto> GetPolicy(Guid policyId);
        public Task<List<PolicyDto>> GetPoliciesByTypeId(Guid PolicyTypeId);
        public Task<bool> DeletePolicy(Guid policyId);
    }
}
