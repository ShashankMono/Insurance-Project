using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface ICommissionService
    {
        public Task<List<CommissionDto>> GetCommissions();

        public Task<List<CommissionDto>> GetCommissionByAgentId(Guid Id);
        public Task<Guid> AddCommission(CommissionDto commissionDto, double amountPaid);
    }
}
