using Insurance_final_project.Dto;
using Insurance_final_project.Models;
using Insurance_final_project.PagingFiles;
using Insurance_final_project.Services;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("customer/{customerId}"), Authorize(Roles = "Customer")]
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

        [HttpGet,Authorize(Roles ="Employee")]
        public async Task<IActionResult> GetAllQuery([FromQuery] PageParameters pageParameter)
        {
            var queries = await _queryService.GetAllQuery();

            var pagedData = PageList<QueryDto>.ToPagedList(queries, pageParameter.PageNumber, pageParameter.PageSize);

            return Ok(new
            {
                Success = true,
                Data = pagedData,
                totalItems = pagedData.TotalCount,
                pageNumber = pagedData.CurrentPage,
                pagesize = pagedData.PageSize,
                totalPages = pagedData.TotalPages,
                Message = "Queries retrieved successfully."
            });
        }

        [HttpPost, Authorize(Roles = "Customer")]
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

        [HttpPut,Authorize(Roles ="Employee")]
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
