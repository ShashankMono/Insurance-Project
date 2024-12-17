using Insurance_final_project.Dto;
using Insurance_final_project.Models;
using Insurance_final_project.PagingFiles;
using Insurance_final_project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Pkcs;

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

        [HttpGet("{policyAccountId}"), Authorize(Roles = "Customer,Admin,Agent,Employee")]
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

        [HttpGet, Authorize(Roles = "Customer,Admin,Agent,Employee")]
        public async Task<IActionResult> GetAllPolicyAccounts([FromQuery] PageParameters pageParameter,
            [FromQuery] string? searchQuery)
        {
            var policyAccounts = await _policyAccountService.GetAllPolicyAccounts(searchQuery);

            var pagedData = PageList<PolicyAccountResponseDto>.ToPagedList(policyAccounts, pageParameter.PageNumber, pageParameter.PageSize);

            return Ok(new
            {
                Success = true,
                Data = pagedData,
                totalItems = pagedData.TotalCount,
                pageNumber = pagedData.CurrentPage,
                pagesize = pagedData.PageSize,
                totalPages = pagedData.TotalPages,
                Message = "Policy accounts retrieved successfully."
            });
        }

        [HttpPost, Authorize(Roles = "Customer")]
        public async Task<IActionResult> CreatePolicyAccount([FromBody] PolicyAccountDto policyAccountDto)
        {
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

        [HttpGet("agent/{agentId}"), Authorize(Roles = "Admin,Agent")]
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

        [HttpGet("customer/{customerId}"), Authorize(Roles = "Customer,Admin,Employee")]
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

        [HttpPost("approve"),Authorize(Roles ="Employee")]
        public IActionResult ApproveAccount([FromBody] ApprovalDto approval)
        {

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

            var response = _policyAccountService.ApproveAccount(approval);

            return Ok(new
            {
                Success = true,
                Data = response,
                Message = "Policy scheme account updated successfully."
            });
        }
    }
}
