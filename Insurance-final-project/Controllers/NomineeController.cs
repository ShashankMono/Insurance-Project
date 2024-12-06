using Insurance_final_project.Dto;
using Insurance_final_project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_final_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NomineeController : ControllerBase
    {
        private readonly INomineeService _nomineeService;

        public NomineeController(INomineeService nomineeService)
        {
            _nomineeService = nomineeService;
        }

        [HttpPost]
        public async Task<IActionResult> AddNominee([FromBody] NomineeDto nominee)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "Invalid model state.",
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
                });
            }

            var nomineeId = await _nomineeService.AddNominee(nominee);
            return Ok(new
            {
                Success = true,
                Data = nomineeId,
                Message = "Nominee added successfully."
            });
        }

        [HttpGet("{nomineeId}")]
        public async Task<IActionResult> GetNominee(Guid nomineeId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "Invalid model state.",
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
                });
            }

            var nominee = await _nomineeService.GetNominee(nomineeId);
            return Ok(new
            {
                Success = true,
                Data = nominee,
                Message = "Nominee retrieved successfully."
            });
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetNominees(Guid customerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "Invalid model state.",
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
                });
            }

            var nominees = await _nomineeService.GetNominees(customerId);
            return Ok(new
            {
                Success = true,
                Data = nominees,
                Message = "Nominees retrieved successfully."
            });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateNominee([FromBody] NomineeDto nominee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "Invalid model state.",
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
                });
            }

            var nomineeId = await _nomineeService.UpdateNominee(nominee);
            return Ok(new
            {
                Success = true,
                Data = nomineeId,
                Message = "Nominee updated successfully."
            });
        }

        [HttpDelete("{nomineeId}")]
        public async Task<IActionResult> DeleteNominee(Guid nomineeId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "Invalid model state.",
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
                });
            }

            bool isDeleted = await _nomineeService.Delete(nomineeId);
            return Ok(new
            {
                Success = isDeleted,
                Message = isDeleted ? "Nominee deleted successfully." : "Nominee not found."
            });
        }
    }
}
