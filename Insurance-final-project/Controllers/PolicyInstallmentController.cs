using Insurance_final_project.Dto;
using Insurance_final_project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_final_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyInstallmentController : ControllerBase
    {
        private readonly IPolicyInstallmentService _policyInstallmentService;

        public PolicyInstallmentController(IPolicyInstallmentService policyInstallmentService)
        {
            _policyInstallmentService = policyInstallmentService;
        }

        // Add Installments
        [HttpPost]
        public IActionResult AddInstallments(PolicyInstallmentDto installmentData)
        {
            _policyInstallmentService.AddInstallments(installmentData);

            return Ok(new
            {
                Success = true,
                Data = (object)null,
                Message = "Installments added successfully."
            });
        }

        // Pay an Installment
        [HttpPost("pay/{installmentId}")]
        public async Task<IActionResult> PayInstallment(Guid installmentId, Guid customerId)
        {
            var result = await _policyInstallmentService.PayInstallment(installmentId, customerId);

            return Ok(new
            {
                Success = true,
                Data = result,
                Message = "Installment payment successful."
            });
        }


        [HttpGet("policyaccount/{policyAccountId}")]
        public async Task<IActionResult> GetInstallmentsByPolicyAccountId(Guid policyAccountId)
        {
            var installments = await _policyInstallmentService.GetInstallmentsByPolicyAccountId(policyAccountId);

            return Ok(new
            {
                Success = true,
                Data = installments,
                Message = "Installments retrieved successfully."
            });
        }
    }
}
