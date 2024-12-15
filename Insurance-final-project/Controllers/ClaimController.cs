using Insurance_final_project.Dto;
using Insurance_final_project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_final_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimController : ControllerBase
    {
        private readonly IClaimService _claimService;

        public ClaimController(IClaimService claimService)
        {
            _claimService = claimService;
        }

        [HttpGet]
        public async Task<IActionResult> GetClaimAccounts()
        {
            var claims = await _claimService.GetClaimAccounts();

            return Ok(new
            {
                Success = true,
                Data = claims,
                Message = "Claim accounts retrieved successfully."
            });
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetClaimAccountsByCustomerId(Guid customerId)
        {
            var claims = await _claimService.GetClaimByCustomerId(customerId);

            return Ok(new
            {
                Success = true,
                Data = claims,
                Message = "Claim accounts retrieved successfully."
            });
        }

        [HttpPost("approve")]
        public async Task<IActionResult> ClaimApproval([FromBody] ApprovalDto claim)
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

            var claimId = await _claimService.ClaimApproval(claim);

            return Ok(new
            {
                Success = true,
                Data = claimId,
                Message = "Claim approval status updated successfully."
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddClaimPolicy( [FromBody] ClaimDto claimDto)
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

            var claimId = await _claimService.AddClaimPolicy( claimDto);

            return Ok(new
            {
                Success = true,
                Data = claimId,
                Message = "Claim added to policy account successfully."
            });
        }

        [HttpPost("Withdrawal")]
        public async Task<IActionResult> ClaimWithdrawal([FromBody] ClaimDto claimDto)
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

            var transactionId = await _claimService.CLaimWithdrawal(claimDto);

            return Ok(new
            {
                Success = true,
                Data = transactionId,
                Message = "Claim withdrawal processed successfully."
            });
        }
    }
}
