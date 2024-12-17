using Insurance_final_project.Dto;
using Insurance_final_project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_final_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommissionController : ControllerBase
    {
        private readonly ICommissionService _commissionService;

        public CommissionController(ICommissionService commissionService)
        {
            _commissionService = commissionService;
        }

        [HttpGet,Authorize(Roles ="Agent,Admin,Employee")]
        public async Task<IActionResult> GetCommissions()
        {
            var commissions = await _commissionService.GetCommissions();

            return Ok(new
            {
                Success = true,
                Data = commissions,
                Message = "Commissions retrieved successfully."
            });
        }

        [HttpGet("{agentId}"), Authorize(Roles = "Agent,Admin,Employee")]
        public async Task<IActionResult> GetCommissionByAgentId(Guid agentId)
        {
            var commissions = await _commissionService.GetCommissionByAgentId(agentId);

            return Ok(new
            {
                Success = true,
                Data = commissions,
                Message = "Commissions for the agent retrieved successfully."
            });
        }

        [HttpPost, Authorize(Roles = "Employee")]
        public async Task<IActionResult> AddCommission([FromBody] CommissionDto commissionDto, [FromQuery] double amountPaid)
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

            var commissionId = await _commissionService.AddCommission(commissionDto, amountPaid);

            return Ok(new
            {
                Success = true,
                Data = commissionId,
                Message = "Commission added successfully."
            });
        }
    }
}
