using Insurance_final_project.Dto;
using Insurance_final_project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_final_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly IAgentService _agentService;

        public AgentController(IAgentService agentService)
        {
            _agentService = agentService;
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent(AgentDto agentDto)
        {
            var agent = _agentService.RegisterAgent(agentDto);
            return Ok(agent);
        }

        [HttpGet("{agentId}")]
        public IActionResult GetAgentById(Guid agentId)
        {
            var agent = _agentService.GetAgentById(agentId);
            return Ok(agent);
        }

        [HttpPost("recommendPlan")]
        public IActionResult RecommendPlan(Guid customerId, Guid policyId, Guid agentId, PolicyAccountDto policyAccountDto)
        {
            _agentService.RecommendPlan(customerId, policyId, agentId, policyAccountDto);
            return Ok("Plan recommended successfully.");
        }

        [HttpGet("commissionWithdrawals/{agentId}")]
        public IActionResult GetCommissionWithdrawals(Guid agentId)
        {
            var withdrawals = _agentService.GetCommissionWithdrawals(agentId);
            return Ok(withdrawals);
        }

        [HttpPost("withdrawCommission")]
        public IActionResult WithdrawCommission(Guid agentId, double amount)
        {
            try
            {
                _agentService.WithdrawCommission(agentId, amount);
                return Ok("Commission withdrawal requested.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("totalCommission/{agentId}")]
        public IActionResult ViewTotalCommission(Guid agentId)
        {
            var commission = _agentService.ViewTotalCommission(agentId);
            return Ok(commission);
        }
    }
}
