using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface ICommissionWithdrawService
    {
        public Guid AddCommissionWithdrawal(CommissionWithdrawalDto commissionWithdrawalRequest);
        public List<CommissionWithdrawalDto> GetCommissionsWithdrawal();
        public Guid UpdateWithdraws(string withdrawStatus);
    }
}
