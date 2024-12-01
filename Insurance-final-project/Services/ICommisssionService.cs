using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface ICommisssionService
    {
        public List<CommissionDto> GetCommissions();
        public Guid AddCommission(CommissionDto commission);
        public List<CommissionDto> CommissionGetById(Guid id);
    }
}
