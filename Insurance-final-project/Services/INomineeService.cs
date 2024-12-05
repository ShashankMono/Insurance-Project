using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface INomineeService
    {
        public Task<Guid> AddNominee(NomineeDto nominee);

        public Task<Guid> UpdateNominee(NomineeDto nominee);
        public Task<List<NomineeDto>> GetNominees(Guid customerId);
        public Task<NomineeDto> GetNominee(Guid nomineeId);
        public Task<bool> Delete(Guid nomineeId);
    }
}
