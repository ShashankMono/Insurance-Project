using AutoMapper;
using Insurance_final_project.Constant;
using Insurance_final_project.Dto;
using Insurance_final_project.Exceptions;
using Insurance_final_project.Models;
using Insurance_final_project.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Insurance_final_project.Services
{
    public class AgentService : IAgentService
    {
        private readonly IRepository<Agent> _agentRepository;
        private readonly IMapper _Mapper;
        private readonly IRepository<Role> _RoleRepo;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public AgentService(
            IRepository<Agent> agentRepository,
            IRepository<Role> roleRepo,
            IUserService UserService,
            IEmailService emailService,
            IMapper mapper)
        {
            _agentRepository = agentRepository;
            _Mapper = mapper;
            _RoleRepo = roleRepo;
            _userService = UserService;
            _emailService = emailService;
        }

        public AgentInputDto GetAgentById(Guid agentId)
        {
            var agent = _agentRepository.Get(agentId);
            if (agent == null)
                throw new Exception("Agent not found.");
            return _Mapper.Map<AgentInputDto>(agent);
        }


        public double ViewTotalCommission(Guid agentId)
        {
            var agent = _agentRepository.Get(agentId);
            if (agent == null)
                throw new Exception("Agent not found.");
            return agent.CommissionEarned;
        }

        public async Task<UserDto> AddAgent(AgentInputDto newAgent)
        {
            Agent agent = _Mapper.Map<AgentInputDto, Agent>(newAgent);
            UserDto user = _userService.AddNewUser(_RoleRepo.GetAll().FirstOrDefault(r => r.RoleName == "Agent").RoleId);
            agent.UserId = user.UserId;
            Agent agentAdded = _agentRepository.Add(agent);
            return user;
        }

        public async Task<List<AgentInputDto>> GetAllAgents()
        {
            return _Mapper.Map<List<Agent>, List<AgentInputDto>>(_agentRepository.GetAll().ToList());
        }

        public async Task<Guid> UpdateAgent(AgentInputDto agent)
        {
            if(_agentRepository.GetAll().AsNoTracking().FirstOrDefault(a=>a.AgentId==agent.AgentId) == null)
            {
                throw new InvalidGuidException("Agent no found!");
            }
            return _agentRepository.Update(_Mapper.Map<Agent>(agent)).AgentId;
        }
    }
}
