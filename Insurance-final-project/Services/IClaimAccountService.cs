using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface IClaimAccountService
    {
        public Guid AddClaimAccount(ClaimDto claim);
        public List<ClaimDto> GetClaimAccounts();
        public Guid ClaimApproval(string claimStatus);
    }
}
