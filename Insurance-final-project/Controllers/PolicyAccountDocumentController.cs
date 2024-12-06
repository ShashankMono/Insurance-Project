using Insurance_final_project.Dto;
using Insurance_final_project.Services;
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

        [HttpPost]
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(Guid id)
        {
            var success = await _service.DeleteDocument(id);
            if (!success)
                return NotFound(new { Success = false, Message = "Document not found." });

            return Ok(new { Success = true, Message = "Document deleted successfully." });
        }

        [HttpGet("{policyAccountId}")]
        public async Task<IActionResult> GetDocuments(Guid policyAccountId)
        {
            var documents = await _service.GetDocuments(policyAccountId);
            return Ok(new { Success = true, Data = documents, Message = "Documents retrieved successfully." });
        }

        [HttpPut]
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
    }
}
