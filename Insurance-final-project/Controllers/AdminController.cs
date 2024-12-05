using Insurance_final_project.Dto;
using Insurance_final_project.Exceptions;
using Insurance_final_project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        // Add Admin
        [HttpPost("add")]
        public async Task<IActionResult> AddAdmin([FromBody] AdminDto adminDto)
        {
            // Check if the model state is valid
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(new { Success = false, Data = (object)null, Message = "Validation failed.", Errors = errors });
            }

            var adminId = await _adminService.AddAdmin(adminDto);

            return Ok(new
            {
                Success = true,
                Data = adminId,
                Message = "Admin added successfully."
            });
        }

        // Update Admin
        [HttpPut("update")]
        public async Task<IActionResult> UpdateAdmin([FromBody] AdminDto adminDto)
        {
            // Check if the model state is valid
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(new { Success = false, Data = (object)null, Message = "Validation failed.", Errors = errors });
            }

            var adminId = await _adminService.UpdateAdmin(adminDto);

            return Ok(new
            {
                Success = true,
                Data = adminId,
                Message = "Admin updated successfully."
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdmin(Guid id)
        {

            var admin = await _adminService.GetAdmin(id);

            return Ok(new
            {
                Success = true,
                Data = admin,
                Message = "Admin retrieved successfully."
            });

        }
    }
}
