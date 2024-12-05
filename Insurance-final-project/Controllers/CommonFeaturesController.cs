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

        [HttpGet("approval")]
        public IActionResult GetApprovalTypes()
        {
            var approvalTypes = _commonService.GetapprovalTypes();
            return Ok(new
            {
                Success = true,
                Data = approvalTypes,
                Message = "Approval types retrieved successfully."
            });
        }


        [HttpGet("Account_status")]
        public IActionResult GetPolicyAccountStatus()
        {
            var accountStatus = _commonService.GetPolicyAccountStatus();
            return Ok(new
            {
                Success = true,
                Data = accountStatus,
                Message = "Policy account statuses retrieved successfully."
            });
        }

        [HttpGet("transaction_status")]
        public IActionResult GetTransactionStatus()
        {
            var transactionTypes = _commonService.GetTransactionStatus();
            return Ok(new
            {
                Success = true,
                Data = transactionTypes,
                Message = "Transaction statuses retrieved successfully."
            });
        }


        [HttpGet("verification")]
        public IActionResult GetVerificationType()
        {
            var verificationTypes = _commonService.GetVerificationType();
            return Ok(new
            {
                Success = true,
                Data = verificationTypes,
                Message = "Verification types retrieved successfully."
            });
        }


        [HttpGet("installment_types")]
        public IActionResult GetPolicyInstallmentType()
        {
            var installmentTypes = _commonService.GetpolicyInstallmentType();
            return Ok(new
            {
                Success = true,
                Data = installmentTypes,
                Message = "Policy installment types retrieved successfully."
            });
        }


        [HttpGet("document_types")]
        public IActionResult GetDocumentType()
        {
            var documentTypes = _commonService.GetDocumentType();
            return Ok(new
            {
                Success = true,
                Data = documentTypes,
                Message = "Document types retrieved successfully."
            });
        }
    }
}
