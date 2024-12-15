using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface IPolicyCancelService
    {
        public Task<List<PolicyCancelReponseDto>> GetPolicyCancels(Guid customerId, string? searchQuery);
        public Task<Guid> ApprovePolicyCancelation(ApprovalDto policyCancel);
        public Task<bool> CancelPolicy(Guid policyAccountId);
        public Task<List<PolicyCancelReponseDto>> GetAllPolicyCancels( string? searchQuery);
    }
}
