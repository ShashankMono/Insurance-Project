using Insurance_final_project.Dto;
using Insurance_final_project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_final_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        private readonly IQueryService _queryService;

        public QueryController(IQueryService queryService)
        {
            _queryService = queryService;
        }

        // Get queries by customer ID
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetQueryByCustomerId(Guid customerId)
        {
            var queries = await _queryService.GetQueryByCustomerId(customerId);
            return Ok(new
            {
                Success = true,
                Data = queries,
                Message = "Queries retrieved successfully."
            });
        }

        [HttpPost]
        public async Task<IActionResult> SubmitQuery([FromBody] QueryDto queryDto)
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
            var queryId = await _queryService.SubmitQuery(queryDto);
            return Ok(new
            {
                Success = true,
                Data = queryId,
                Message = "Query submitted successfully."
            });
        }

        [HttpPut]
        public async Task<IActionResult> ResponseToQuery([FromBody] QueryDto queryDto)
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
            var queryId = await _queryService.ResponseToQuery(queryDto);
            return Ok(new
            {
                Success = true,
                Data = queryId,
                Message = "Query response updated successfully."
            });
        }
    }
}
