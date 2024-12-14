using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface IClaimService
    {
        Task<List<ClaimDto>> GetClaimAccounts();
        Task<Guid> ClaimApproval(ApprovalDto claim);
        public Task<Guid> AddClaimPolicy( ClaimDto claimDto);
        public Task<Guid> CLaimWithdrawal(ClaimDto claimDto);
        public Task<List<ClaimDto>> GetClaimByCustomerId(Guid customerId);
    }
}
