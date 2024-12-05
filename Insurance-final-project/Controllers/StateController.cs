using Insurance_final_project.Dto;
using Insurance_final_project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_final_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IStateService _stateService;

        public StateController(IStateService stateService)
        {
            _stateService = stateService;
        }


        [HttpGet]
        public async Task<IActionResult> GetStates()
        {
            var states = await _stateService.GetStates();
            return Ok(new
            {
                Success = true,
                Data = states,
                Message = "States retrieved successfully."
            });
        }


        [HttpPost]
        public async Task<IActionResult> AddState([FromBody] StateDto stateDto)
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
            var stateId = await _stateService.AddState(stateDto);
            return Ok(new
            {
                Success = true,
                Data = stateId,
                Message = "State added successfully."
            });
        }


        [HttpPut]
        public async Task<IActionResult> UpdateState([FromBody] StateDto stateDto)
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
            var stateId = await _stateService.UpdateState(stateDto);
            return Ok(new
            {
                Success = true,
                Data = stateId,
                Message = "State updated successfully."
            });
        }
    }
}
