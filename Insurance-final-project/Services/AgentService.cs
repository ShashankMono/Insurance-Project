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

        public AgentResponseDto GetAgentById(Guid agentId)
        {
            var agent = _agentRepository.Get(agentId);
            if (agent == null)
                throw new InvalidGuidException("Agent not found!");
            return _Mapper.Map<AgentResponseDto>(agent);
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
            var RoleId = _RoleRepo.GetAll().FirstOrDefault(r => r.RoleName == "Agent").RoleId;
            if (RoleId == null)
            {
                throw new RoleNotFoundException("Role not found! Please add role \"Agent\"");
            }
            UserDto user = _userService.AddNewUser(RoleId);
            agent.UserId = user.UserId;
            Agent agentAdded = _agentRepository.Add(agent);
            _emailService.SendUserDetailthroughEmail(agentAdded.Email, "Agent username and password", user);
            return user;
        }

        public async Task<List<AgentResponseDto>> GetAllAgents()
        {
            return _Mapper.Map<List<Agent>, List<AgentResponseDto>>(_agentRepository.GetAll().ToList());
        }

        public async Task<Guid> UpdateAgent(AgentInputDto agentInput)
        {
            var agent = _Mapper.Map<AgentResponseDto>(agentInput);
            var ExistingAgent = _agentRepository.GetAll().AsNoTracking().FirstOrDefault(a => a.AgentId == agent.AgentId);
            if (ExistingAgent == null)
            {
                throw new InvalidGuidException("Agent no found!");
            }
            agent.UserId = ExistingAgent.UserId;
            agent.CommissionEarned = ExistingAgent.CommissionEarned;
            agent.TotalCommission = ExistingAgent.TotalCommission;
            return _agentRepository.Update(_Mapper.Map<Agent>(agent)).AgentId;
        }
    }
}