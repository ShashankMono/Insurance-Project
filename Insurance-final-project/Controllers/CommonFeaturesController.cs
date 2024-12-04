using Insurance_final_project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonFeaturesController : ControllerBase
    {
        private readonly ICommonService _commonService;

        public CommonFeaturesController(ICommonService commonService)
        {
            _commonService = commonService;
        }

        private void ValidateModel()
        {
            if (!ModelState.IsValid)
            {
                throw new ValidationException("Invalid model state.");
            }
        }

        [HttpGet("approval-types")]
        public async Task<IActionResult> GetApprovalTypes()
        {
            ValidateModel();
            var approvalTypes = await _commonService.GetapprovalTypes();
            return Ok(new { Success = true, Data = approvalTypes, Message = "Approval types fetched successfully." });
        }

        [HttpGet("cities")]
        public async Task<IActionResult> GetCities()
        {
            ValidateModel();
            var cities = await _commonService.GetCities();
            return Ok(new { Success = true, Data = cities, Message = "Cities fetched successfully." });
        }

        [HttpGet("policies")]
        public async Task<IActionResult> GetPolicies()
        {
            ValidateModel();
            var policies = await _commonService.GetPolicies();
            return Ok(new { Success = true, Data = policies, Message = "Policies fetched successfully." });
        }

        [HttpGet("policy-account-status")]
        public async Task<IActionResult> GetPolicyAccountStatus()
        {
            ValidateModel();
            var statuses = await _commonService.GetPolicyAccountStatus();
            return Ok(new { Success = true, Data = statuses, Message = "Policy account statuses fetched successfully." });
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            ValidateModel();
            var roles = await _commonService.GetRoles();
            return Ok(new { Success = true, Data = roles, Message = "Roles fetched successfully." });
        }

        [HttpGet("states")]
        public async Task<IActionResult> GetStates()
        {
            ValidateModel();
            var states = await _commonService.GetStates();
            return Ok(new { Success = true, Data = states, Message = "States fetched successfully." });
        }

        [HttpGet("transaction-status")]
        public async Task<IActionResult> GetTransactionStatus()
        {
            ValidateModel();
            var statuses = await _commonService.GetTransactionStatus();
            return Ok(new { Success = true, Data = statuses, Message = "Transaction statuses fetched successfully." });
        }

        [HttpGet("verification-types")]
        public async Task<IActionResult> GetVerificationTypes()
        {
            ValidateModel();
            var verificationTypes = await _commonService.GetVerificationType();
            return Ok(new { Success = true, Data = verificationTypes, Message = "Verification types fetched successfully." });
        }

        [HttpGet("policy-types")]
        public async Task<IActionResult> GetPolicyTypes()
        {
            ValidateModel();
            var policyTypes = await _commonService.GetPolicyType();
            return Ok(new { Success = true, Data = policyTypes, Message = "Policy types fetched successfully." });
        }

        [HttpGet("policy-installment-types")]
        public async Task<IActionResult> GetPolicyInstallmentTypes()
        {
            ValidateModel();
            var installmentTypes = await _commonService.GetpolicyInstallmentType();
            return Ok(new { Success = true, Data = installmentTypes, Message = "Policy installment types fetched successfully." });
        }
    }
}
