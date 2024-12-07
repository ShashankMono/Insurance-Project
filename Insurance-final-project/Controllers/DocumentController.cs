using Insurance_final_project.Dto;
using Insurance_final_project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_final_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }


        [HttpPost]
        public async Task<IActionResult> AddDocument([FromBody] DocumentDto document)
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

            var documentId = await _documentService.AddDocument(document);

            return Ok(new
            {
                Success = true,
                Data = documentId,
                Message = "Document added successfully."
            });
        }


        [HttpPut("approve")]
        public async Task<IActionResult> ChangeApproveStatus([FromBody] VerificationDto document)
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

            var documentId = await _documentService.ChangeApproveStatus(document);

            return Ok(new
            {
                Success = true,
                Data = documentId,
                Message = "Document approval status updated successfully."
            });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDoc([FromBody] DocumentDto document)
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

            var documentId = await _documentService.UpdateDocument(document);

            return Ok(new
            {
                Success = true,
                Data = documentId,
                Message = "Document approval status updated successfully."
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetDocuments()
        {
            var documents = await _documentService.GetDocument();

            return Ok(new
            {
                Success = true,
                Data = documents,
                Message = "Documents retrieved successfully."
            });
        }
    }
}
