using Insurance_final_project.Dto;
using Insurance_final_project.Models;

namespace Insurance_final_project.Services
{
    public interface IAgentService
    {
        public AgentInputDto GetAgentById(Guid agentId);

        public Task<UserDto> AddAgent(AgentInputDto agent);
        public Task<List<AgentInputDto>> GetAllAgents();
        public double ViewTotalCommission(Guid agentId);
        public Task<Guid> UpdateAgent(AgentInputDto agent);
        

    }
}
