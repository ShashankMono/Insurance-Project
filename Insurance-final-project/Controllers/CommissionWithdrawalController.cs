using Insurance_final_project.Dto;
using Insurance_final_project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_final_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommissionWithdrawalController : ControllerBase
    {
        private readonly ICommissionWithdrawalService _commissionWithdrawalService;

        public CommissionWithdrawalController(ICommissionWithdrawalService commissionWithdrawalService)
        {
            _commissionWithdrawalService = commissionWithdrawalService;
        }

        [HttpGet,Authorize(Roles ="Agent,Admin")]
        public async Task<IActionResult> GetCommissionsWithdrawal()
        {
            var withdrawals = await _commissionWithdrawalService.GetCommissionsWithdrawal();

            return Ok(new
            {
                Success = true,
                Data = withdrawals,
                Message = "Commission withdrawals retrieved successfully."
            });
        }


        [HttpPost,Authorize(Roles ="Agent")]
        public async Task<IActionResult> AddWithdrawalRequest([FromBody] CommissionWithdrawalDto withdrawRequest)
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

            var withdrawalId = await _commissionWithdrawalService.AddWithdrawalRequest(withdrawRequest);

            return Ok(new
            {
                Success = true,
                Data = withdrawalId,
                Message = "Withdrawal request added successfully."
            });
        }

        [HttpGet("{agentId}"), Authorize(Roles = "Admin,Agent")]
        public async Task<IActionResult> GetCommissionWithdrawalByAgentId(Guid agentId)
        {
            var withdrawals = await _commissionWithdrawalService.GetCommissionWithdrawalByAgentId(agentId);

            return Ok(new
            {
                Success = true,
                Data = withdrawals,
                Message = "Commission withdrawals for the agent retrieved successfully."
            });
        }
    }
}
