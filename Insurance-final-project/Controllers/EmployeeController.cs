using Insurance_final_project.Dto;
using Insurance_final_project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        private void ValidateModel()
        {
            if (!ModelState.IsValid)
            {
                throw new ValidationException("Invalid model state.");
            }
        }

        [HttpPost("add-agent")]
        public async Task<IActionResult> AddAgent([FromBody] AgentDto newAgent)
        {
            ValidateModel();
            var user = await _employeeService.AddAgent(newAgent);
            return Ok(new { Success = true, Data = user, Message = "Agent added successfully." });
        }

        [HttpPut("change-approve-status")]
        public async Task<IActionResult> ChangeApproveStatus([FromBody] DocumentDto document)
        {
            ValidateModel();
            var documentId = await _employeeService.ChangeApproveStatus(document);
            return Ok(new { Success = true, Data = documentId, Message = "Document approval status updated successfully." });
        }

        [HttpGet("get-agent-report/{agentId}")]
        public async Task<IActionResult> GetAgentReport(Guid agentId)
        {
            var agent = await _employeeService.GetAgentReport(new AgentDto { AgentId = agentId });
            return Ok(new { Success = true, Data = agent, Message = "Agent report retrieved successfully." });
        }

        [HttpGet("get-all-agents")]
        public async Task<IActionResult> GetAllAgents()
        {
            var agents = await _employeeService.GetAllAgents();
            return Ok(new { Success = true, Data = agents, Message = "All agents retrieved successfully." });
        }

        [HttpGet("get-all-policy-accounts")]
        public async Task<IActionResult> GetAllPolicyAccounts()
        {
            var policyAccounts = await _employeeService.GetAllPolicyAccounts();
            return Ok(new { Success = true, Data = policyAccounts, Message = "Policy accounts retrieved successfully." });
        }

        [HttpGet("get-claim-accounts")]
        public async Task<IActionResult> GetClaimAccounts()
        {
            var claims = await _employeeService.GetClaimAccounts();
            return Ok(new { Success = true, Data = claims, Message = "Claim accounts retrieved successfully." });
        }

        [HttpGet("get-commissions")]
        public async Task<IActionResult> GetCommissions()
        {
            var commissions = await _employeeService.GetCommissions();
            return Ok(new { Success = true, Data = commissions, Message = "Commissions retrieved successfully." });
        }

        [HttpGet("get-commissions-withdrawal")]
        public async Task<IActionResult> GetCommissionsWithdrawal()
        {
            var withdrawals = await _employeeService.GetCommissionsWithdrawal();
            return Ok(new { Success = true, Data = withdrawals, Message = "Commission withdrawals retrieved successfully." });
        }

        [HttpGet("get-customer-accounts")]
        public async Task<IActionResult> GetCustomerAccounts()
        {
            var customers = await _employeeService.GetCustomerAccounts();
            return Ok(new { Success = true, Data = customers, Message = "Customer accounts retrieved successfully." });
        }

        [HttpGet("get-policies")]
        public async Task<IActionResult> GetPolicies()
        {
            var policies = await _employeeService.GetPolicies();
            return Ok(new { Success = true, Data = policies, Message = "Policies retrieved successfully." });
        }

        [HttpGet("get-policy/{policyId}")]
        public async Task<IActionResult> GetPolicy(Guid policyId)
        {
            var policy = await _employeeService.GetPolicy(policyId);
            return Ok(new { Success = true, Data = policy, Message = "Policy retrieved successfully." });
        }

        [HttpGet("get-policy-account/{policyAccountId}")]
        public async Task<IActionResult> GetPolicyAccount(Guid policyAccountId)
        {
            var policyAccount = await _employeeService.GetPolicyAccount(new PolicyAccountDto { Id = policyAccountId });
            return Ok(new { Success = true, Data = policyAccount, Message = "Policy account retrieved successfully." });
        }

        [HttpGet("get-policy-cancels")]
        public async Task<IActionResult> GetPolicyCancels()
        {
            var policyCancels = await _employeeService.GetPolicyCancels();
            return Ok(new { Success = true, Data = policyCancels, Message = "Policy cancellations retrieved successfully." });
        }

        [HttpGet("get-transactions")]
        public async Task<IActionResult> GetTransactions()
        {
            var transactions = await _employeeService.GetTransactions();
            return Ok(new { Success = true, Data = transactions, Message = "Transactions retrieved successfully." });
        }

        [HttpPut("update-employee-profile")]
        public async Task<IActionResult> UpdateEmployeeProfile([FromBody] EmployeeDto employee)
        {
            ValidateModel();
            var employeeId = await _employeeService.UpdateEmployeeProfile(employee);
            return Ok(new { Success = true, Data = employeeId, Message = "Employee profile updated successfully." });
        }
    }
}
