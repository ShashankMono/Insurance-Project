using Insurance_final_project.Dto;
using Insurance_final_project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_final_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyAccountDocumentController : ControllerBase
    {
        private readonly IPolicyAccountDocumentService _service;

        public PolicyAccountDocumentController(IPolicyAccountDocumentService service)
        {
            _service = service;
        }

        [HttpPost,Authorize(Roles ="Customer")]
        public async Task<IActionResult> AddDocument([FromBody] PolicyAccountDocumentDto document)
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
                    Message = "Invalid model state.",
                    Errors = errors
                });
            }

            var documentId = await _service.AddDocument(document);
            return Ok(new { Success = true, Data = documentId, Message = "Document added successfully." });
        }

        [HttpDelete("{id}"), Authorize(Roles = "Customer")]
        public async Task<IActionResult> DeleteDocument(Guid id)
        {
            var success = await _service.DeleteDocument(id);
            if (!success)
                return NotFound(new { Success = false, Message = "Document not found." });

            return Ok(new { Success = true, Message = "Document deleted successfully." });
        }

        [HttpGet("{policyAccountId}"), Authorize(Roles = "Customer,Employee")]
        public async Task<IActionResult> GetDocuments(Guid policyAccountId)
        {
            var documents = await _service.GetDocuments(policyAccountId);
            return Ok(new { Success = true, Data = documents, Message = "Documents retrieved successfully." });
        }

        [HttpPut,Authorize(Roles ="Customer")]
        public async Task<IActionResult> UpdateDocument([FromBody] UpdateDocumentDto updateDoc)
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
                    Message = "Invalid model state.",
                    Errors = errors
                });
            }

            var documentId = await _service.UpdateDocument(updateDoc);
            return Ok(new { Success = true, Data = documentId, Message = "Document updated successfully." });
        }

        [HttpGet("customer/{accountId}"),Authorize(Roles ="Customer,Employee")]
        public async Task<IActionResult> GetDocumentsByCustomerId(Guid accountId)
        {
            var documents = await _service.GetDocumentByAccountId(accountId);

            return Ok(new
            {
                Success = true,
                Data = documents,
                Message = "Document retrived successfully."
            });
        }

        [HttpPut("approve"),Authorize(Roles ="Employee")]
        public async Task<IActionResult> ChangeApproveStatus([FromBody] VerificationDto verifyInfo)
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

            var documentId = await _service.ChangeApproveStatus(verifyInfo);

            return Ok(new
            {
                Success = true,
                Data = documentId,
                Message = "Document approval status updated successfully."
            });
        }
    }
}
