using Insurance_final_project.Dto;
using Insurance_final_project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_final_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService=emailService;
        }

        [HttpPost("Marketing"), Authorize(Roles = "Agent")]

        public IActionResult SendMarketingMail(MarketingDto info)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(new { Success = false,
                    Data = (object)null,
                    Message = "Validation failed.",
                    Errors = errors 
                });
            }

            _emailService.SendMarketingMail(info);
            return Ok(new { Success = true,
                Data = (object)null,
                Message = "Email sended successfully!" 
            });
        }
    }
}
