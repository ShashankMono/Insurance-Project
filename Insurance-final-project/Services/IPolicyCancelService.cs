using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface IPolicyCancelService
    {
        public Task<List<PolicyCancelReponseDto>> GetPolicyCancels(Guid customerId);
        public Task<Guid> ApprovePolicyCancelation(ApprovalDto policyCancel);
        public Task<bool> CancelPolicy(Guid policyAccountId);
    }
}
