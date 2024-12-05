using Insurance_final_project.Dto;
using Insurance_final_project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_final_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyAccountController : ControllerBase
    {
        private readonly IPolicyAccountService _policyAccountService;

        public PolicyAccountController(IPolicyAccountService policyAccountService)
        {
            _policyAccountService = policyAccountService;
        }

        // Get Policy Account By ID
        [HttpGet("{policyAccountId}")]
        public async Task<IActionResult> GetPolicyAccountById(Guid policyAccountId)
        {
            var policyAccount = await _policyAccountService.GetPolicyAccountById(policyAccountId);

            return Ok(new
            {
                Success = true,
                Data = policyAccount,
                Message = "Policy account retrieved successfully."
            });
        }

        // Get All Policy Accounts
        [HttpGet("all")]
        public async Task<IActionResult> GetAllPolicyAccounts()
        {
            var policyAccounts = await _policyAccountService.GetAllPolicyAccounts();

            return Ok(new
            {
                Success = true,
                Data = policyAccounts,
                Message = "Policy accounts retrieved successfully."
            });
        }

        // Create Policy Account
        [HttpPost("create")]
        public async Task<IActionResult> CreatePolicyAccount([FromBody] PolicyAccountDto policyAccountDto)
        {
            // Validate model state
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new
                {
                    Success = false,
                    Data = (object)null,
                    Message = "Validation failed.",
                    Errors = errors
                });
            }

            var policyAccountId = await _policyAccountService.CreatePolicyAccount(policyAccountDto);

            return Ok(new
            {
                Success = true,
                Data = policyAccountId,
                Message = "Policy account created successfully."
            });
        }

        // Get Policy Accounts By Agent ID
        [HttpGet("agent/{agentId}")]
        public async Task<IActionResult> GetPolicyAccountsByAgent(Guid agentId)
        {
            var policyAccounts = await _policyAccountService.GetPolicyAccountsByAgent(agentId);

            return Ok(new
            {
                Success = true,
                Data = policyAccounts,
                Message = "Policy accounts for the agent retrieved successfully."
            });
        }

        // Get Policies By Customer ID
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetPoliciesByCustomer(Guid customerId)
        {
            var policies = await _policyAccountService.GetPoliciesByCustomer(customerId);

            return Ok(new
            {
                Success = true,
                Data = policies,
                Message = "Policies for the customer retrieved successfully."
            });
        }
    }
}
