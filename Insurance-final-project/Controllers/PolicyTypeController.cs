using Insurance_final_project.Dto;
using Insurance_final_project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_final_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyTypeController : ControllerBase
    {
        private readonly IPolicyTypeService _policyTypeService;

        public PolicyTypeController(IPolicyTypeService policyTypeService)
        {
            _policyTypeService = policyTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPolicyTypes()
        {
            var policyTypes = await _policyTypeService.GetPolicyType();
            return Ok(new
            {
                Success = true,
                Data = policyTypes,
                Message = "Policy types retrieved successfully."
            });
        }

        [HttpPost,Authorize(Roles ="Admin")]
        public async Task<IActionResult> AddPolicyType([FromBody] PolicyTypeDto policyType)
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
            var policyTypeId = await _policyTypeService.AddPolicyType(policyType);
            return Ok(new
            {
                Success = true,
                Data = policyTypeId,
                Message = "Policy type added successfully."
            });
        }

        [HttpPut,Authorize(Roles ="Admin")]
        public async Task<IActionResult> UpdatePolicyType([FromBody] PolicyTypeDto policyType)
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
            var updatedPolicyTypeId = await _policyTypeService.UpdatePolicyType(policyType);
            return Ok(new
            {
                Success = true,
                Data = updatedPolicyTypeId,
                Message = "Policy type updated successfully."
            });
        }

        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public IActionResult DeletePolicyType(Guid id)
        {
            bool isDeleted = _policyTypeService.DeletePolicyType(id);

            if (isDeleted)
            {
                return Ok(new
                {
                    Success = true,
                    Message = "Policy type deactivated successfully."
                });
            }

            return NotFound(new
            {
                Success = false,
                Message = "Policy type not found."
            });
        }
    }
}
