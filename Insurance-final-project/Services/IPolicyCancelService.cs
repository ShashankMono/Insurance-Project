using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface IPolicyCancelService
    {
        public List<PolicyCancelDto> GetPolicyCancel();
        public Guid UpdatePolicycal(string status);

    }
}
