using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface IPolicyInstallmentService
    {
        public void AddInstallments(Guid policyAccountId);
        public Task<bool> PayInstallment(Guid installmentId);
        public Task<List<PolicyInstallmentResponsDto>> GetInstallmentsByPolicyAccountId(Guid PolicyAccountId);
    }
}
