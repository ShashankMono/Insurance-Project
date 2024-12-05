using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface IPolicyTypeService
    {
        public Task<List<PolicyTypeDto>> GetPolicyType();
        public Task<Guid> AddPolicyType(PolicyTypeDto policyType);
        public Task<Guid> UpdatePolicyType(PolicyTypeDto policyType);

        public bool DeletePolicyType(Guid Id);
    }
}
