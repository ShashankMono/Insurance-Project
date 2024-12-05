using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface IStateService
    {
        Task<Guid> AddState(StateDto state);
        Task<Guid> UpdateState(StateDto state);
        public Task<List<StateDto>> GetStates();
    }
}
