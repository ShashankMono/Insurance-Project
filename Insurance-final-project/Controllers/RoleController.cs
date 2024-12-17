using Insurance_final_project.Dto;
using Insurance_final_project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_final_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleService.GetRoles();
            return Ok(new
            {
                Success = true,
                Data = roles,
                Message = "Roles retrieved successfully."
            });
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRole([FromBody] RoleDto roleDto)
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
            var roleId = await _roleService.AddRole(roleDto);
            return Ok(new
            {
                Success = true,
                Data = roleId,
                Message = "Role added successfully."
            });
        }

        [HttpPut, Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRole([FromBody] RoleDto roleDto)
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
            var roleId = await _roleService.UpdateRole(roleDto);
            return Ok(new
            {
                Success = true,
                Data = roleId,
                Message = "Role updated successfully."
            });
        }
    }
}
