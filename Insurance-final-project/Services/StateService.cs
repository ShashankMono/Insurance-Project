using AutoMapper;
using Insurance_final_project.Dto;
using Insurance_final_project.Exceptions;
using Insurance_final_project.Models;
using Insurance_final_project.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Insurance_final_project.Services
{
    public class StateService:IStateService
    {
        private readonly IRepository<State> _StateRepo;
        private readonly IMapper _Mapper;
        public StateService(IRepository<State> repo, IMapper mapper)
        {
            _StateRepo = repo;
            _Mapper = mapper;
        }
        public async Task<Guid> AddState(StateDto state)
        {
            return _StateRepo.Add(_Mapper.Map<StateDto, State>(state)).StateId;
        }
        public async Task<Guid> UpdateState(StateDto state)
        {
            if (_StateRepo.GetAll().AsNoTracking().FirstOrDefault(s=>s.StateId == state.StateId) == null) {
                throw new InvalidGuidException("State not found!");
            }
            return _StateRepo.Update(_Mapper.Map<StateDto, State>(state)).StateId;
        }

        public async Task<List<StateDto>> GetStates()
        {
            return _Mapper.Map<List<State>, List<StateDto>>(_StateRepo.GetAll().ToList());
        }
    }
}
