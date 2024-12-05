using Insurance_final_project.Dto;
using Insurance_final_project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_final_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private readonly IPolicyService _policyService;

        public PolicyController(IPolicyService policyService)
        {
            _policyService = policyService;
        }


        [HttpPost]
        public async Task<IActionResult> AddPolicy([FromBody] PolicyDto policy)
        {
            var policyId = await _policyService.AddPolicy(policy);

            return Ok(new
            {
                Success = true,
                Data = policyId,
                Message = "Policy added successfully."
            });
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePolicy([FromBody] PolicyDto policy)
        {
            var updatedPolicyId = await _policyService.UpdatePolicy(policy);

            return Ok(new
            {
                Success = true,
                Data = updatedPolicyId,
                Message = "Policy updated successfully."
            });
        }

        // Get all policies
        [HttpGet]
        public async Task<IActionResult> GetPolicies()
        {
            var policies = await _policyService.GetPolicies();

            return Ok(new
            {
                Success = true,
                Data = policies,
                Message = "Policies retrieved successfully."
            });
        }

        [HttpGet("{policyId}")]
        public async Task<IActionResult> GetPolicy(Guid policyId)
        {
            var policy = await _policyService.GetPolicy(policyId);

            if (policy == null)
            {
                return NotFound(new
                {
                    Success = false,
                    Message = "Policy not found."
                });
            }

            return Ok(new
            {
                Success = true,
                Data = policy,
                Message = "Policy retrieved successfully."
            });
        }

        [HttpGet("typeId/{policyTypeId}")]
        public async Task<IActionResult> GetPoliciesByTypeId(Guid policyTypeId)
        {
            var policies = await _policyService.GetPoliciesByTypeId(policyTypeId);

            return Ok(new
            {
                Success = true,
                Data = policies,
                Message = "Policies by type retrieved successfully."
            });
        }
    }
}
