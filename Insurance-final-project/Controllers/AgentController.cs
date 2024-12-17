using Insurance_final_project.Dto;
using Insurance_final_project.Models;
using Insurance_final_project.PagingFiles;
using Insurance_final_project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_final_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly IAgentService _agentService;

        public AgentController(IAgentService agentService)
        {
            _agentService = agentService;
        }

        [HttpGet("{id}")]
        public IActionResult GetAgentById(Guid id)
        {
            var agent = _agentService.GetAgentById(id);

            return Ok(new
            {
                Success = true,
                Data = agent,
                Message = "Agent retrieved successfully."
            });
        }

        [HttpGet("User/{id}")]
        public IActionResult GetAgentByUserId(Guid id)
        {
            var agent = _agentService.GetAgentByUserId(id);

            return Ok(new
            {
                Success = true,
                Data = agent,
                Message = "Agent retrieved successfully."
            });
        }

        [HttpGet("{id}/commission")]
        public IActionResult ViewTotalCommission(Guid id)
        {
            var commission = _agentService.ViewEarnedCommission(id);

            return Ok(new
            {
                Success = true,
                Data = commission,
                Message = "Total commission retrieved successfully."
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddAgent([FromBody] AgentInputDto newAgent)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(new { Success = false, Data = (object)null, Message = "Validation failed.", Errors = errors });
            }

            var user = await _agentService.AddAgent(newAgent);

            return Ok(new
            {
                Success = true,
                Data = user,
                Message = "Agent added successfully."
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAgents([FromQuery] PageParameters pageParameter,
            [FromQuery] string? searchQuery)
        {
            var agents = await _agentService.GetAllAgents(searchQuery);

            var pagedData = PageList<AgentResponseDto>.ToPagedList(agents, pageParameter.PageNumber, pageParameter.PageSize);

            return Ok(new
            {
                Success = true,
                Data = pagedData,
                totalItems = pagedData.TotalCount,
                pageNumber = pagedData.CurrentPage,
                pagesize = pagedData.PageSize,
                totalPages = pagedData.TotalPages,
                Message = "All agents retrieved successfully."
            });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAgent(AgentInputDto agent)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(new { Success = false, Data = (object)null, Message = "Validation failed.", Errors = errors });
            }

            var response = _agentService.UpdateAgent(agent);

            return Ok(new
            {
                Success = true,
                Data = response,
                Message = "All agents retrieved successfully."
            });
        }
    }
}
