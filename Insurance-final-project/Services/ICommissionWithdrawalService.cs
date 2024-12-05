using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface ICommissionWithdrawalService
    {
        public Task<List<CommissionWithdrawalDto>> GetCommissionsWithdrawal();
        //public Task<Guid> ApproveWithdrawal(CommissionWithdrawalDto withdrawRequest);

        public Task<Guid> AddWithdrawalRequest(CommissionWithdrawalDto withdrawRequest);
        public Task<List<CommissionWithdrawalDto>> GetCommissionWithdrawalByAgentId(Guid agendId);

    }
}
