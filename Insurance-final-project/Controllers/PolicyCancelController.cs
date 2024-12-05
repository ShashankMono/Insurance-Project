using Insurance_final_project.Dto;
using Insurance_final_project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_final_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyCancelController : ControllerBase
    {
        private readonly IPolicyCancelService _policyCancelService;

        public PolicyCancelController(IPolicyCancelService policyCancelService)
        {
            _policyCancelService = policyCancelService;
        }

        [HttpPost("cancel/{policyAccountId}")]
        public async Task<IActionResult> CancelPolicy(Guid policyAccountId)
        {
            var result = await _policyCancelService.CancelPolicy(policyAccountId);

            if (!result)
            {
                return BadRequest(new
                {
                    Success = false,
                    Data = (object)null,
                    Message = "Policy cancellation failed. Policy not found or already closed."
                });
            }

            return Ok(new
            {
                Success = true,
                Data = result,
                Message = "Policy cancellation requested successfully."
            });
        }

        [HttpPost("approve")]
        public async Task<IActionResult> ApprovePolicyCancelation([FromBody] ApprovalDto approvalDto)
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

            var policyCancelId = await _policyCancelService.ApprovePolicyCancelation(approvalDto);

            return Ok(new
            {
                Success = true,
                Data = policyCancelId,
                Message = "Policy cancellation approved successfully."
            });
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetPolicyCancels()
        {
            var policyCancels = await _policyCancelService.GetPolicyCancels();

            return Ok(new
            {
                Success = true,
                Data = policyCancels,
                Message = "Policy cancellation records retrieved successfully."
            });
        }
    }
}
