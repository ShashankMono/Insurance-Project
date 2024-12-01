using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface IAgentService
    {
        public Guid AddAgent(AgentDto agent);
        public AgentDto GetAgentReport(AgentDto agent);// incliudes all fields like commissions, commission withdrwal, policy account
        public List<AgentDto> GetAgents();
        public Guid UpdateAgent(AgentDto agent);
        public Guid DeleteAgent(Guid agentId);


    }
}
