using Insurance_final_project.Dto;
using Insurance_final_project.Models;

namespace Insurance_final_project.Services
{
    public interface IAgentService
    {
        public AgentResponseDto GetAgentById(Guid agentId);

        public Task<UserDto> AddAgent(AgentInputDto agent);
        public Task<List<AgentResponseDto>> GetAllAgents();
        public double ViewTotalCommission(Guid agentId);
        public Task<Guid> UpdateAgent(AgentInputDto agent);
        public Task<AgentResponseDto> GetAgentByUserId(Guid UserId);


    }
}