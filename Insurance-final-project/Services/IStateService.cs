using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface IStateService
    {
        public Guid AddState(StateDto State);
        public Guid UpdateState(StateDto State);
        public int DeleteState(Guid StateId);
        public List<StateDto> GetAllState();
    }
}
