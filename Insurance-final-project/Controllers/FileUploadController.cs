using Insurance_final_project.Dto;
using Insurance_final_project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_final_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IFileUploadService _uploadFileService;
        public FileUploadController(IFileUploadService fileUpload)
        {
            _uploadFileService = fileUpload;
        }

        [HttpPost, Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> UploadFile([FromForm] GetFileDto fileDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(new { Success = false, Data = (object)null, Message = "Validation failed.", Errors = errors });
            }


            var response = _uploadFileService.Uploadfile(fileDto);

            return Ok(new
            {
                Success = true,
                Data = response,
                Message = "File uploaded successfully."
            });
        }
    }
}
