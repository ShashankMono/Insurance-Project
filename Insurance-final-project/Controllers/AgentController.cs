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


        [HttpGet("{agentId}")]
        public IActionResult GetAgentById(Guid agentId)
        {
            var agent = _agentService.GetAgentById(agentId);
            return Ok(agent);
        }


        [HttpGet("commissionWithdrawals/{agentId}")]
        public IActionResult GetCommissionWithdrawals(Guid agentId)
        {
            var withdrawals = _agentService.GetCommissionWithdrawals(agentId);
            return Ok(withdrawals);
        }

        [HttpPost("withdrawCommission")]
        public IActionResult WithdrawCommission(CommissionWithdrawalDto commissionWithdrawalDto)
        {
            _agentService.WithdrawCommission(commissionWithdrawalDto.AgentId, commissionWithdrawalDto.Amount);
            return Ok("Commission withdrawal requested.");
            
        }

        [HttpGet("totalCommission/{agentId}")]
        public IActionResult ViewTotalCommission(Guid agentId)
        {
            var commission = _agentService.ViewTotalCommission(agentId);
            return Ok(commission);
        }
        [HttpGet("{agentId}/policyAccounts")]
        public IActionResult GetPolicyAccountsByAgent(Guid agentId)
        {
            var policyAccounts = _agentService.GetPolicyAccountsByAgent(agentId);
            return Ok(policyAccounts);
            
        }
    }
}
